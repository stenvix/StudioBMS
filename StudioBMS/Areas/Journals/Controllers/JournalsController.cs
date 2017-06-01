using System;
using System.Collections.Generic;
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

        [HttpGet,HttpGet("{date}")]
        public async Task<IActionResult> Index(DateTime? date = null)
        {
            if (!date.HasValue)
            {
                date = DateTime.Today;
            }

            ViewData["Workshops"] = await _workshopManager.GetAsync();
            ViewData["Workers"] = await _personManager.GetEmployees();

            List<Guid> ids = new List<Guid>();
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

            return View(await _personManager.GetWithPerformerOrders(ids.ToArray()));
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
            return RedirectToAction("Index");
        }
    }
}