using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.Managers.Identity;
using StudioBMS.Business.Managers.Models.Interfaces;

namespace StudioBMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOrderManager _orderManager;
        private readonly IPersonManager _personManager;
        private readonly IWorkshopManager _workshopManager;
        private readonly IServiceManager _serviceManager;
        private readonly PersonModelManager _personModelManager;

        public HomeController(IOrderManager orderManager,
            IPersonManager personManager,
            IWorkshopManager workshopManager,
            IServiceManager serviceManager, PersonModelManager personModelManager)
        {
            _orderManager = orderManager;
            _personManager = personManager;
            _workshopManager = workshopManager;
            _serviceManager = serviceManager;
            _personModelManager = personModelManager;
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

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> Order()
        {
            var model = new OrderViewModel();
            model.Date = DateTime.Now;
            
            ViewData["Workshops"] = await _workshopManager.GetAsync();

            ViewData["Performers"] = await _personManager.GetEmployees();

            ViewData["Services"] = await _serviceManager.GetAsync();

            return View(model);
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Order(OrderViewModel model)
        {
            var customer = await _personModelManager.FindByEmailAsync(model.EMail);
            if (customer == null)
            {
                var person = new PersonModel
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.EMail,
                    Email = model.EMail,
                    Workshop = new WorkshopModel {Id = model.WorkshopId}
                };
                var result = await _personModelManager.CreateAsync(person);
                if (result.Succeeded)
                {
                    customer = await _personModelManager.FindByEmailAsync(model.EMail);
                    model.CustomerId = customer.Id;
                }
            }
            else
            {
                model.CustomerId = customer.Id;
            }
            await _orderManager.CreateAsync(model);
            return RedirectToAction("Index");
        }

        [NonAction]
        private bool IsNotInRoles(string[] roles)
        {
            var result = true;
            foreach (var role in roles)
            {
                result &= !User.IsInRole(role);
            }
            return result;
        }
    }
}