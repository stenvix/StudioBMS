using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.Managers.Identity;
using StudioBMS.Business.Managers.Models.Interfaces;
using StudioBMS.Models;

namespace StudioBMS.Areas.Workers.Controllers
{
    [Area("Workers")]
    [Route("[area]")]
    public class WorkersController : Controller
    {
        private readonly IPersonManager _personManager;
        private readonly PersonModelManager _permonModelManager;
        private readonly ITimeTableManager _timeTableManager;
        private readonly IServiceManager _serviceManager;
        private readonly IRoleManager _roleManager;
        private readonly IWorkshopManager _workshopManager;
        private readonly IOrderManager _orderManager;
        private readonly IHtmlLocalizer<ModelResource> _modelLocalizer;

        public WorkersController(IPersonManager personManager,
            PersonModelManager permonModelManager,
            ITimeTableManager timeTableManager,
            IServiceManager serviceManager,
            IRoleManager roleManager,
            IWorkshopManager workshopManager,
            IOrderManager orderManager,
            IHtmlLocalizer<ModelResource> modelLocalizer)
        {
            _personManager = personManager;
            _permonModelManager = permonModelManager;
            _timeTableManager = timeTableManager;
            _serviceManager = serviceManager;
            _roleManager = roleManager;
            _workshopManager = workshopManager;
            _orderManager = orderManager;
            _modelLocalizer = modelLocalizer;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IList<PersonModel> employees = null;
            if (User.IsInRole(StringConstants.AdministratorRole))
            {
                employees = await _personManager.GetStaff();
            }
            else if (User.IsInRole(StringConstants.ManagerRole))
            {
                var manager = await _personManager.FindByName(User.Identity.Name);
                employees = await _personManager.GetEmployees(manager.Workshop.Id);
            }
            else
            {
                employees = await _personManager.GetEmployees();
            }
            foreach (var employee in employees)
            {
                employee.TimeTables = await _timeTableManager.FindByWorker(employee.Id);
                employee.Services = await _serviceManager.FindByPerson(employee.Id);
            }
            return View(employees);
        }

        [HttpGet("{workerId}/time")]
        public async Task<IActionResult> TimeIndex(Guid workerId)
        {
            var worker = await _personManager.GetAsync(workerId);
            ViewData["WorkshopTime"] = await _workshopManager.GetTimeTables(worker.Workshop.Id);
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
            else
            {
                ViewData["Blocked"] = (await _timeTableManager.FindByWorker(id)).Select(i => i.WeekDay);
            }
            var worker = await _personManager.GetAsync(id);

            ViewData["WorkshopTime"] = await _workshopManager.GetTimeTables(worker.Workshop.Id);
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
            return RedirectToAction("TimeIndex", new { workerId });
        }

        [HttpGet("{workerId}/service")]
        public async Task<IActionResult> ServiceIndex(Guid workerId)
        {
            ViewData["workerId"] = workerId;
            ViewData["Services"] = await _serviceManager.GetAsync();
            var model = await _serviceManager.FindByPerson(workerId);
            return View(model);
        }
        [HttpPost("{workerId}/service")]
        public async Task<IActionResult> ServiceSubmit(Guid workerId, Guid serviceId)
        {
            await _serviceManager.CreatePerson(workerId, serviceId);
            return RedirectToAction("ServiceIndex", new { workerId });
        }

        [HttpGet("delete/{workerId}/service/{serviceId}")]
        public async Task<IActionResult> DeleteService(Guid workerId, Guid serviceId)
        {
            await _serviceManager.DeletePersonService(workerId, serviceId);
            return RedirectToAction("ServiceIndex", new { workerId });
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (User.IsInRole(StringConstants.AdministratorRole))
            {
                ViewData["Roles"] = await _roleManager.GetAsync();
                ViewData["Workshops"] = await _workshopManager.GetAsync();
            }
            else
            {
                ViewData["Roles"] = await _roleManager.GetWorkerRoles();
            }
            return View(await _personManager.GetAsync(id));
        }

        [HttpPost("edit")]
        public async Task<IActionResult> Update(PersonModel model)
        {
            var personModel = await _personManager.GetAsync(model.Id);
            PersonMapping(model, personModel);

            //TODO: Add message
            var result = await _permonModelManager.UpdateAsync(personModel);

            if (!result.Succeeded)
                NotFound();

            await _roleManager.ClearRoles(personModel.Id);

            await _roleManager.AddToRole(model.Id, model.Role.Id);

            return RedirectToAction("Index");
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            if (User.IsInRole(StringConstants.AdministratorRole))
            {
                ViewData["Roles"] = await _roleManager.GetAsync();
            }
            else
            {
                ViewData["Roles"] = await _roleManager.GetWorkerRoles();
            }
            ViewData["Workshops"] = await _workshopManager.GetAsync();
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(PersonModel model)
        {
            if (User.IsInRole(StringConstants.ManagerRole))
            {
                var manager = await _personManager.FindByName(User.Identity.Name);
                model.Workshop = manager.Workshop;
            }
            var roleId = model.Role.Id;
            model.UserName = model.Email;
            await _permonModelManager.CreateAsync(model);
            model = await _permonModelManager.FindByEmailAsync(model.Email);
            await _roleManager.AddToRole(model.Id, roleId);
            return RedirectToAction("Index");
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _personManager.DeleteAsync(id);
            return RedirectToAction("Index");
        }


        #region JSON


        [HttpPost("json"), AllowAnonymous]
        public async Task<IActionResult> Json(Guid workshopId)
        {
            var employees = await _personManager.GetEmployees(workshopId);
            foreach (var employee in employees)
            {
                employee.Role.LocalizedName = _modelLocalizer[employee.Role.Name].Value;
            }
            return new JsonResult(employees);
        }

        [HttpPost("time"), AllowAnonymous]
        public async Task<IActionResult> Time(Guid workerId)
        {
            var minDate = DateTime.Now.AddMinutes(30);
            var maxDate = DateTime.Now.AddMonths(1);

            var weekdays = new[] { DayOfWeek.Sunday, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday };
            var timetables = await _timeTableManager.FindByWorker(workerId);

            var result = new List<Object>();

            result.AddRange(await _orderManager.GetDisabledTimespans(workerId, minDate, maxDate));

            var disabledDays = weekdays.Except(timetables.Select(i => i.WeekDay));
            for (var date = minDate.Date; date.Date < maxDate.Date; date = date.AddDays(1))
            {
                if (disabledDays.Contains(date.DayOfWeek))
                    continue;
                var timetable = timetables.FirstOrDefault(i => i.WeekDay == date.DayOfWeek);

                var start = date.Date;
                var end = date.Date.AddTicks(timetable.Start.Ticks);
                result.Add(new { Start = start, End = end });
                start = date.Date.AddTicks(timetable.End.Ticks);
                end = date.Date.AddDays(1);
                result.Add(new { Start = start, End = end });
            }
            return new JsonResult(new { DisabledDays = disabledDays, DisabledTimespans = result });
        }

        #endregion



        private void PersonMapping(PersonModel newModel, PersonModel oldModel)
        {
            oldModel.FirstName = newModel.FirstName;
            oldModel.LastName = newModel.LastName;
            oldModel.Email = newModel.Email;
            oldModel.Birthday = newModel.Birthday;
            oldModel.UserName = newModel.Email;
            oldModel.Workshop.Id = newModel.Workshop.Id;
        }
    }
}