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
using StudioBMS.Models;
using StudioBMS.Pages.Titles;
using StudioBMS.Services;

namespace StudioBMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOrderManager _orderManager;
        private readonly IPersonManager _personManager;
        private readonly IWorkshopManager _workshopManager;
        private readonly PersonModelManager _personModelManager;
        private readonly IHtmlLocalizer<MessageResource> _messageLocalizer;
        private readonly IHtmlLocalizer<PageTitleResource> _titleLocalizer;
        private readonly IEmailSender _emailSender;

        public HomeController(IOrderManager orderManager,
            IPersonManager personManager,
            IWorkshopManager workshopManager,
            IServiceManager serviceManager,
            PersonModelManager personModelManager,
            IHtmlLocalizer<MessageResource> messageLocalizer,
            IHtmlLocalizer<PageTitleResource> titleLocalizer,
            IEmailSender emailSender)
        {
            _orderManager = orderManager;
            _personManager = personManager;
            _workshopManager = workshopManager;
            _personModelManager = personModelManager;
            _messageLocalizer = messageLocalizer;
            _titleLocalizer = titleLocalizer;
            _emailSender = emailSender;
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
            var model = new OrderViewModel { Date = DateTime.Today };

            ViewData["Workshops"] = await _workshopManager.GetAsync();

            return View(model);
        }

        [HttpPost("order"), AllowAnonymous]
        public async Task<IActionResult> Order(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = await _personModelManager.FindByEmailAsync(model.EMail);
                Task emailTask = Task.FromResult(0);

                if (customer == null)
                {
                    var person = new PersonModel
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        UserName = model.EMail,
                        Email = model.EMail,
                        PhoneNumber = model.Phone,
                        Workshop = new WorkshopModel { Id = model.WorkshopId }
                    };
                    var password = PasswordHelper.GetRandomPassword() +"!";
                    var result = await _personModelManager.CreateAsync(person, password);
                    if (result.Succeeded)
                    {
                        customer = await _personManager.FindByEmail(model.EMail);
                        await _personModelManager.AddToRoleAsync(customer, StringConstants.CustomerRole);
                        model.CustomerId = customer.Id;
                        var message = $"{_messageLocalizer.GetString("RegistrationMessage").Value}. Login: {customer.Email} <br /> Password: {password}";
                        emailTask = _emailSender.SendEmailAsync(customer.Email, _titleLocalizer["Registration"].Value, message);
                    }
                }
                else
                {
                    model.CustomerId = customer.Id;
                }

                var order = await _orderManager.CreateAsync(model);

                var liqpay = LiqPayViewModel.GetModel(order);
                liqpay.ResultUrl = Url.Action("Callback", "Orders", null, Request.Scheme);
                liqpay.Description = _messageLocalizer["OrderDescription", order.Services.Select(i => i.Title)].Value;
                await emailTask;
                return PartialView("Payment/OrderPayment", new OrderPaymentViewModel { LiqPay = liqpay, Order = order });
            }
            return View("Order", model);
        }

        [HttpGet("thanks"), AllowAnonymous]
        public async Task<IActionResult> Thanks(Guid id)
        {
            return View(await _orderManager.GetAsync(id));
        }
    }
}