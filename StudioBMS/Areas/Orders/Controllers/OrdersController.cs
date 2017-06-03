using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudioBMS.Business.DTO.Extensions;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.DTO.Models.ViewModels;
using StudioBMS.Business.Managers.Models.Interfaces;

namespace StudioBMS.Areas.Orders.Controllers
{
    [Area("Orders")]
    [Route("[area]")]
    public class OrdersController : Controller
    {
        private readonly IOrderManager _orderManager;
        private readonly IPersonManager _personManager;
        private readonly IServiceManager _serviceManager;
        private readonly IWorkshopManager _workshopManager;

        public OrdersController(IOrderManager orderManager,
            IPersonManager personManager,
            IWorkshopManager workshopManager,
            IServiceManager serviceManager)
        {
            _orderManager = orderManager;
            _personManager = personManager;
            _workshopManager = workshopManager;
            _serviceManager = serviceManager;
        }

        public async Task<IActionResult> Index()
        {
            //ViewData["Message"] = TempData["Message"];
            IList<OrderModel> orders = null;

            if (User.IsInRole(StringConstants.CustomerRole))
            {
                var customer = await _personManager.FindByName(User.Identity.Name);
                orders = await _orderManager.FindByCustomer(customer.Id);
            }
            else if (User.IsInRole(StringConstants.ManagerRole))
            {
                var manager = await _personManager.FindByName(User.Identity.Name);
                orders = await _orderManager.FindByWorkshop(manager.Workshop.Id);
            }
            else
            {
                orders = await _orderManager.GetAsync();
            }

            return View(orders);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            var model = new OrderViewModel();
            model.Date = DateTime.Now;
            PersonModel user = null;
            if (!User.IsInRole("Administrator"))
            {
                user = await _personManager.FindByName(User.Identity.Name);
                if (user == null)
                    throw new ArgumentNullException($"Person not found: {nameof(user)}");
            }

            if (User.IsInRole(StringConstants.CustomerRole))
                model.CustomerId = user.Id;
            else
                ViewData["Customers"] = await _personManager.GetCustomers();

            if (IsNotInRoles(new[] { StringConstants.CustomerRole, StringConstants.AdministratorRole }))
                model.WorkshopId = user.Workshop.Id;
            else
                ViewData["Workshops"] = await _workshopManager.GetAsync();

            if (IsNotInRoles(new[]
                {StringConstants.CustomerRole, StringConstants.ManagerRole, StringConstants.AdministratorRole}))
            {
                model.PerformerId = user.Id;
            }
            else
            {
                if (model.WorkshopId == Guid.Empty)
                    ViewData["Performers"] = await _personManager.GetEmployees();
                else
                    ViewData["Performers"] = await _personManager.GetEmployees(model.WorkshopId);
            }

            if (model.PerformerId == Guid.Empty)
                ViewData["Services"] = await _serviceManager.GetAsync();
            else
                ViewData["Services"] = await _serviceManager.FindByPerson(model.PerformerId);

            return View(model);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(OrderViewModel order)
        {
            //TempData["Message"] = new MessageViewModel{Type = MessageType.Success, Message = "OrderSuccess"};
            await _orderManager.CreateAsync(order);
            return RedirectToAction("Index");
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(Guid id, string redirectUrl)
        {
            var model = await _orderManager.GetAsync(id);
            ViewData["Redirect"] = redirectUrl;
            ViewData["Action"] = BusinessAction.Update;
            ViewData["Customers"] = await _personManager.GetCustomers();
            ViewData["Workshops"] = await _workshopManager.GetAsync();
            ViewData["Performers"] = await _personManager.GetEmployees();
            ViewData["Services"] = await _serviceManager.GetAsync();
            ViewData["Statuses"] = await _orderManager.GetStatuses();
            return View(model.To<OrderViewModel>());
        }

        [HttpPost("edit")]
        public async Task<IActionResult> Update(OrderViewModel order, string redirectUrl)
        {
            //TODO: Send email message on change

            var model = order.To<OrderModel>();
            await _orderManager.UpdateAsync(model);
            if (string.IsNullOrEmpty(redirectUrl))
                return RedirectToAction("Index");
            return new RedirectResult(redirectUrl);
        }

        [HttpGet("pay/{id}")]
        public IActionResult Payment(Guid id)
        {
            return View(new PaymentViewModel { Id = id });
        }

        [HttpPost("pay")]
        public async Task<IActionResult> SubmitPayment(PaymentViewModel payment)
        {
            await _orderManager.ManageBalance(payment);
            return RedirectToAction("Index");
        }

        [HttpGet("decline/{id}")]
        public async Task<IActionResult> Decline(Guid id, string redirectUrl)
        {
            await _orderManager.Deactivate(id);
            if (string.IsNullOrEmpty(redirectUrl))
                return RedirectToAction("Index");
            return new RedirectResult(redirectUrl);
        }

        [HttpGet("done/{id}")]
        public async Task<IActionResult> Done(Guid id, string redirectUrl)
        {
            await _orderManager.Done(id);
            if (string.IsNullOrEmpty(redirectUrl))
                return RedirectToAction("Index");
            return new RedirectResult(redirectUrl);
        }

        [HttpGet("call")]
        [AllowAnonymous]
        public IActionResult Calback()
        {
            var orderPayment = new OrderPaymentViewModel();
            var model = new OrderModel
            {
                Id = Guid.NewGuid(),
                OrderNumber = 12341234
            };

            var liqpay = new LiqPayViewModel
            {
                PrivateKey = "tuKOtMrz1arqJd2nv9UxtuZ5W9SpFgvdpP1P5MpL",
                Amount = 52.5,
                Language = CultureInfo.CurrentCulture.TwoLetterISOLanguageName,
                OrderId = model.Id.ToString(),
                ResultUrl = Url.Action("Callback", "Orders", null, Request.Scheme)
            };


            //liqpay.ResultUrl = Url.Action("Calback", "Orders", null, Request.Scheme);
            orderPayment.Order = model;
            orderPayment.LiqPay = liqpay;

            return PartialView("Payment/OrderPayment", orderPayment);
        }

        [HttpPost("callback")]
        [AllowAnonymous]
        public async Task<IActionResult> Callback(string data, string signature)
        {
            var stringData = Encoding.UTF8.GetString(Convert.FromBase64String(data));
            var liqCallback = JsonConvert.DeserializeObject<LiqPayCallbackViewModel>(stringData);
            liqCallback.PrivateKey = "tuKOtMrz1arqJd2nv9UxtuZ5W9SpFgvdpP1P5MpL";

            if (liqCallback.GetSignature(data) == signature)
            {
                var order = await _orderManager.GetAsync(liqCallback.OrderId);
                order.Balance += liqCallback.Amount;
                await _orderManager.UpdateAsync(order);

                if (!User.Identity.IsAuthenticated)
                    return RedirectToAction("Thanks", "Home");
                //TODO: Success message;
                return RedirectToAction("Index");
            }
            return new EmptyResult();
        }

        [NonAction]
        private bool IsNotInRoles(string[] roles)
        {
            var result = true;
            foreach (var role in roles)
                result &= !User.IsInRole(role);
            return result;
        }
    }
}