using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.Managers.Models.Interfaces;

namespace StudioBMS.Areas.Workshops.Controllers
{
    [Area("Workshops")]
    [Route("[area]")]
    public class WorkshopsController : Controller
    {
        private readonly IWorkshopManager _workshopManager;
        private readonly ITimeTableManager _timeTableManager;

        public WorkshopsController(IWorkshopManager workshopManager, ITimeTableManager timeTableManager)
        {
            _workshopManager = workshopManager;
            _timeTableManager = timeTableManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _workshopManager.GetAsync());
        }

        [HttpGet("{workshopId}/time")]
        public async Task<IActionResult> TimeIndex(Guid workshopId)
        {
            ViewData["workshopId"] = workshopId;
            return View(await _workshopManager.GetTimeTables(workshopId));
        }

        [HttpGet("{workshopId}/time/form")]
        public async Task<IActionResult> TimeForm(Guid workshopId, Guid? timeTableId)
        {
            ViewData["workshopId"] = workshopId;
            TimeTableModel model = null;
            if (timeTableId.HasValue)
            {
                model = await _timeTableManager.GetAsync(timeTableId.Value);
            }
            return PartialView("_TimeTableForm", model);
        }

        [HttpPost("{workshopId}/time")]
        public IActionResult TimeFormSubmit(Guid workshopId, TimeTableModel model)
        {
            return null;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                _workshopManager?.Dispose();
                _timeTableManager?.Dispose();
            }
        }
    }
}