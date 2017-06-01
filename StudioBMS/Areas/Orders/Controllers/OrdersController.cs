using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudioBMS.Business.DTO.Extensions;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.Managers.Models.Interfaces;
using StudioBMS.Models.UI;

namespace StudioBMS.Areas.Orders.Controllers
{
    [Area("Orders"), Route("[area]")]
    public class OrdersController : Controller
    {
        private readonly IOrderManager _orderManager;
        private readonly IPersonManager _personManager;
        private readonly IWorkshopManager _workshopManager;
        private readonly IServiceManager _serviceManager;

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
            return View(await _orderManager.GetAsync());
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
            {
                model.CustomerId = user.Id;
            }
            else
            {
                ViewData["Customers"] = await _personManager.GetCustomers();
            }

            if (IsNotInRoles(new[] { StringConstants.CustomerRole, StringConstants.AdministratorRole }))
            {
                model.WorkshopId = user.Workshop.Id;
            }
            else
            {
                ViewData["Workshops"] = await _workshopManager.GetAsync();
            }

            if (IsNotInRoles(new[] { StringConstants.CustomerRole, StringConstants.ManagerRole, StringConstants.AdministratorRole }))
            {
                model.PerformerId = user.Id;
            }
            else
            {
                if (model.WorkshopId == Guid.Empty)
                {
                    ViewData["Performers"] = await _personManager.GetEmployees();
                }
                else
                {
                    ViewData["Performers"] = await _personManager.GetEmployees(model.WorkshopId);
                }
            }

            if (model.PerformerId == Guid.Empty)
            {
                ViewData["Services"] = await _serviceManager.GetAsync();
            }
            else
            {
                ViewData["Services"] = await _serviceManager.FindByPerson(model.PerformerId);
            }

            return View(model);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(OrderViewModel order)
        {
            //TempData["Message"] = new MessageViewModel{Type = MessageType.Success, Message = "OrderSuccess"};
            await _orderManager.CreateAsync(order);
            return RedirectToAction("Index");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await _orderManager.GetAsync(id);
            ViewData["Action"] = BusinessAction.Update;
            ViewData["Customers"] = await _personManager.GetCustomers();
            ViewData["Workshops"] = await _workshopManager.GetAsync();
            ViewData["Performers"] = await _personManager.GetEmployees();
            ViewData["Services"] = await _serviceManager.GetAsync();
            ViewData["Statuses"] = await _orderManager.GetStatuses();
            return View(model.To<OrderViewModel>());
        }

        [HttpPost("edit")]
        public async Task<IActionResult> Update(OrderViewModel order)
        {
            //TODO: Send email message on change

            var model = order.To<OrderModel>();
            await _orderManager.UpdateAsync(model);
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