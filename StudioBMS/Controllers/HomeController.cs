using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.DTO.Models.ViewModels;
using StudioBMS.Business.Infrastructure.Helpers;
using StudioBMS.Business.Managers.Identity;
using StudioBMS.Business.Managers.Models.Interfaces;
using StudioBMS.Messages;

namespace StudioBMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOrderManager _orderManager;
        private readonly IPersonManager _personManager;
        private readonly IWorkshopManager _workshopManager;
        private readonly IServiceManager _serviceManager;
        private readonly PersonModelManager _personModelManager;
        private readonly IHtmlLocalizer<MessageResource> _messageLocalizer;

        public HomeController(IOrderManager orderManager,
            IPersonManager personManager,
            IWorkshopManager workshopManager,
            IServiceManager serviceManager, 
            PersonModelManager personModelManager,
            IHtmlLocalizer<MessageResource> messageLocalizer)
        {
            _orderManager = orderManager;
            _personManager = personManager;
            _workshopManager = workshopManager;
            _serviceManager = serviceManager;
            _personModelManager = personModelManager;
            _messageLocalizer = messageLocalizer;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        [HttpGet("order"), AllowAnonymous]
        public async Task<IActionResult> Order()
        {
            var model = new OrderViewModel {Date = DateTime.Today};

            ViewData["Workshops"] = await _workshopManager.GetAsync();

            return View(model);
        }

        [HttpPost("order"), AllowAnonymous]
        public async Task<IActionResult> Order(OrderViewModel model)
        {
            if (ModelState.IsValid) { 
            var customer = await _personModelManager.FindByEmailAsync(model.EMail);
            if (customer == null)
            {
                var person = new PersonModel
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.EMail,
                    Email = model.EMail,
                    PhoneNumber = model.Phone,
                    Workshop = new WorkshopModel {Id = model.WorkshopId}
                };
                var password = PasswordHelper.GetRandomPassword();
                var result = await _personModelManager.CreateAsync(person, password+"!");
                if (result.Succeeded)
                {
                    customer = await _personManager.FindByEmail(model.EMail);
                    await _personModelManager.AddToRoleAsync(customer, StringConstants.CustomerRole);
                    model.CustomerId = customer.Id;
                    //TODO: Send message to email with account credentials
                }
            }
            else
            {
                model.CustomerId = customer.Id;
            }
            var order = await _orderManager.CreateAsync(model);

            var liqpay = new LiqPayViewModel
            {
                OrderId = order.Id.ToString(),
                PrivateKey = "tuKOtMrz1arqJd2nv9UxtuZ5W9SpFgvdpP1P5MpL",
                Amount = order.Price,
                ResultUrl = Url.Action("Callback", "Orders", null, Request.Scheme),
                Description = _messageLocalizer["OrderDescription", order.Services.Select(i=>i.Title) ].Value
            };
                
            return PartialView("Payment/OrderPayment", new OrderPaymentViewModel{LiqPay = liqpay, Order = order});
            }
            return View("Order", model);
        }

        [HttpGet("thanks"), AllowAnonymous]
        public IActionResult Thanks()
        {
            return View();
        }
    }
}