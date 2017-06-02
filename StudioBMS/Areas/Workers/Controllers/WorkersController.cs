using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.Managers.Identity;
using StudioBMS.Business.Managers.Models.Interfaces;

namespace StudioBMS.Areas.Workers.Controllers
{
    [Area("Workers")]
    [Route("[area]")]
    public class WorkersController : Controller
    {
        private readonly IPersonManager _personManager;
        private readonly PersonModelManager _permonModelManager;
        private readonly ITimeTableManager _timeTableManager;

        public WorkersController(IPersonManager personManager, PersonModelManager permonModelManager, ITimeTableManager timeTableManager)
        {
            _personManager = personManager;
            _permonModelManager = permonModelManager;
            _timeTableManager = timeTableManager;
        }

        public async Task<IActionResult> Index()
        {
            IList<PersonModel> employees = null;
            if (User.IsInRole("Administrator"))
            {
                employees = await _personManager.GetStaff();
            }
            else
            {
                var manager = await _permonModelManager.FindByNameAsync(User.Identity.Name);
                employees = await _personManager.GetEmployees(manager.Workshop.Id);
            }
            return View(employees);
        }

        [HttpGet("{workerId}/time")]
        public async Task<IActionResult> TimeIndex(Guid workerId)
        {
            ViewData["workerId"] = workerId;
            return View(await _timeTableManager.FindByWorker(workerId));
        }
        [HttpGet("{id}/time/form")]
        public async Task<IActionResult> TimeForm(Guid id, Guid? timetableId)
        {
            ViewData["WorkerId"] = id;
            TimeTableModel model = null;
            if (timetableId.HasValue)
            {
                model = await _timeTableManager.GetAsync(timetableId.Value);
            }
            return PartialView("WorkerTimeForm", model);
        }
        [HttpPost("{workerId}/time")]
        public async Task<IActionResult> TimeFormSubmit(Guid workerId, TimeTableModel model)
        {
            if (model.Id == Guid.Empty)
            {
                model = await _timeTableManager.CreateAsync(model);
                await _timeTableManager.CreateWorker(workerId, model.Id);
            }
            else
            {
                await _timeTableManager.UpdateAsync(model);
            }
           
            return RedirectToAction("TimeIndex", new { workerId });
        }

        [HttpGet("delete/{workerId}/time/{timetableId}")]
        public async Task<IActionResult> DeleteTimetable(Guid workerId, Guid timetableId)
        {
            //TODO: ADD MESSAGE
            await _timeTableManager.DeleteAsync(timetableId);
            return RedirectToAction("TimeIndex", new {workerId});
        }
    }
}