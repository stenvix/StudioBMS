using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudioBMS.Business.DTO.Models.ViewModels;
using StudioBMS.Business.Managers.Models.Interfaces;
using StudioBMS.Core.Entities.Statistics;

namespace StudioBMS.Areas.Statistics.Controllers
{
    [Area("Statistics"), Route("[area]")]
    public class StatisticsController : Controller
    {
        private readonly IWorkshopManager _workshopManager;
        private readonly IOrderManager _orderManager;
        private readonly IPersonManager _personManager;

        public StatisticsController(IWorkshopManager workshopManager, IOrderManager orderManager, IPersonManager personManager)
        {
            _workshopManager = workshopManager;
            _orderManager = orderManager;
            _personManager = personManager;
        }
        [HttpGet("{category}")]
        public async Task<IActionResult> Index(string category = "workshop", Guid[] ids = default(Guid[]))
        {
            var user = await _personManager.FindByName(User.Identity.Name);
            var statisticViewModel = new StatisticViewModel
            {

                PeriodStart = DateTime.Now.AddMonths(-1).Date,
                PeriodEnd = DateTime.Now.Date
            };

            if (User.IsInRole(StringConstants.CustomerRole))
            {
                category = StringConstants.CustomersCategory;
                ViewData["Person"] = user;
            }
            else if (User.IsInRole(StringConstants.ManagerRole))
            {
                ViewData["Workshop"] = user.Workshop;
            }
            else if(!User.IsInRole(StringConstants.AdministratorRole))
            {
                category = StringConstants.WorkersCategory;
                ViewData["Person"] = user;
            }

            if (category == StringConstants.CustomersCategory && !User.IsInRole(StringConstants.CustomerRole))
            {
                ViewData["Customers"] = await _personManager.GetCustomers();
            }

            if (category == StringConstants.WorkersCategory && !User.IsInRole(StringConstants.CustomerRole))
            {
                if (User.IsInRole(StringConstants.AdministratorRole))
                {
                    ViewData["Workers"] = await _personManager.GetEmployees();
                }
                if (User.IsInRole(StringConstants.ManagerRole))
                {
                    ViewData["Workshop"] = user.Workshop;
                    ViewData["Workers"] = await _personManager.GetEmployees(user.Workshop.Id);
                }
            }
            if (category == StringConstants.WorkshopsCategory && User.IsInRole(StringConstants.AdministratorRole))
            {
                ViewData["Workshops"] = await _workshopManager.GetAsync();
            }
            ViewData["Ids"] = ids;
            ViewData["Category"] = category;
            return View(statisticViewModel);
        }

        [HttpPost("{category}")]
        public async Task<IActionResult> CategoriesStatistics(string category, StatisticViewModel model)
        {
            if (category == StringConstants.CustomersCategory)
            {
                return new JsonResult(await _orderManager.StatisticsByCustomer(model.Ids, model.PeriodStart, model.PeriodEnd));
            }
            if (category == StringConstants.WorkersCategory)
            {
                return new JsonResult(await _orderManager.StatisticsByWorkers(model.Ids, model.PeriodStart, model.PeriodEnd));
            }
            if (category == StringConstants.WorkshopsCategory)
            {
                return new JsonResult(await _orderManager.StatisticsByWorkshops(model.Ids, model.PeriodStart, model.PeriodEnd));
            }

            return new JsonResult(new { });
        }
    }
}