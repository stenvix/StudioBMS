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
        public async Task<IActionResult> Index(string category = "workshop")
        {
            var user = await _personManager.FindByName(User.Identity.Name);
            var statisticViewModel = new StatisticViewModel
            {

                PeriodStart = DateTime.Now.AddMonths(-1).Date,
                PeriodEnd = DateTime.Now.Date
            };

            if (User.IsInRole(StringConstants.CustomerRole))
            {
                if (category != StringConstants.CustomersCategory)
                    category = StringConstants.CustomersCategory;
                ViewData["Customer"] = user;
            }

            if (User.IsInRole(StringConstants.AdministratorRole))
            {
                ViewData["Workshops"] = await _workshopManager.GetAsync();
                ViewData["Workers"] = await _personManager.GetStaff();
            }
            if (User.IsInRole(StringConstants.ManagerRole))
            {
                ViewData["Workshop"] = user.Workshop;
                ViewData["Workers"] = await _personManager.GetEmployees(user.Workshop.Id);
            }

            ViewData["Category"] = category;
            return View(statisticViewModel);
        }

        [HttpPost("customers")]
        public async Task<IActionResult> Customers(StatisticViewModel model)
        {
            List<Statistic> statistic = new List<Statistic>();
            foreach (var customer in model.Ids)
            {
                statistic.Add(await _orderManager.StatisticsByCustomer(customer, model.PeriodStart, model.PeriodEnd));
            }
            return new JsonResult(statistic);
        }
    }
}