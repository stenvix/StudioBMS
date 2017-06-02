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
            return PartialView("WorkshopTimeForm", model);
        }

        [HttpPost("{workshopId}/time")]
        public async Task<IActionResult> TimeFormSubmit(Guid workshopId, TimeTableModel model)
        {
            if (model.Id == Guid.Empty)
            {
                model = await _timeTableManager.CreateAsync(model);
                await _timeTableManager.CreateWorkshop(workshopId, model.Id);
            }
            else
            {
                await _timeTableManager.UpdateAsync(model);
            }
            return RedirectToAction("TimeIndex");
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(WorkshopModel model)
        {
            //TODO: Add message
            await _workshopManager.CreateAsync(model);
            return RedirectToAction("Index");
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            return View(await _workshopManager.GetAsync(id));
        }

        [HttpPost("edit")]
        public async Task<IActionResult> Update(WorkshopModel model)
        {
            //TODO: Add message
            await _workshopManager.UpdateAsync(model);
            return RedirectToAction("Index");
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            //TODO: Add message
            await _workshopManager.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet("delete/{workshopId}/time/{timetableId}")]
        public async Task<IActionResult> DeleteTimetable(Guid workshopId, Guid timetableId)
        {
            await _timeTableManager.DeleteAsync(timetableId);
            return RedirectToAction("TimeIndex", new { workshopId });
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