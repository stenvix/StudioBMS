﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudioBMS.Business.DTO.Models.ViewModels;
using StudioBMS.Business.Managers.Models.Interfaces;

namespace StudioBMS.Areas.Journals.Controllers
{
    [Area("Journals"), Route("[area]")]
    public class JournalsController : Controller
    {
        private readonly IWorkshopManager _workshopManager;
        private readonly IPersonManager _personManager;

        public JournalsController(IWorkshopManager workshopManager, IPersonManager personManager)
        {
            _workshopManager = workshopManager;
            _personManager = personManager;
        }

        [HttpGet, HttpGet("{date}")]
        public async Task<IActionResult> Index(DateTime? date = null)
        {
            DateTime filterDate;
            filterDate = date ?? DateTime.Now;
            ViewData["Date"] = filterDate;
            bool isWorker = false;

            List<Guid> ids = new List<Guid>();

            if (User.IsInRole(StringConstants.AdministratorRole) || User.IsInRole(StringConstants.ManagerRole))
            {
                ViewData["Workshops"] = await _workshopManager.GetAsync();
                if (User.IsInRole(StringConstants.AdministratorRole))
                {
                    ViewData["Workers"] = await _personManager.GetEmployees();
                }
                else
                {
                   var person = await _personManager.FindByName(User.Identity.Name);
                    ViewData["Workers"] = await _personManager.GetEmployees(person.Workshop.Id);
                }
                if (HttpContext.Request.Cookies.ContainsKey("journal"))
                {
                    string cookie = HttpContext.Request.Cookies["journal"];
                    var stringIds = cookie.Split(';');
                    foreach (var stringId in stringIds)
                    {
                        Guid id;
                        if (Guid.TryParse(stringId, out id))
                        {
                            ids.Add(id);
                        }
                    }
                }
            }
            else
            {
                isWorker = true;
                var id = Guid.Parse(User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier).Value);
                ids.Add(id);
            }
            ViewData["IsWorker"] = isWorker;
            return View(await _personManager.GetWithPerformerOrders(ids.ToArray(), filterDate, isWorker));
        }

        [HttpPost]
        public IActionResult Index(JournalViewModel model)
        {
            if (model.WorkerIds != null)
            {
                string ids = string.Join(";", model.WorkerIds);
                Response.Cookies.Append("journal", ids);
            }
            else
            {
                Response.Cookies.Append("journal", "");
            }
            return RedirectToAction("Index", new { date = model.Date.ToString("yyyy-MM-dd") });
        }
    }
}