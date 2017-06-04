using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.Managers.Identity;
using StudioBMS.Business.Managers.Models.Interfaces;

namespace StudioBMS.Areas.Clients.Controllers
{
    [Area("Clients")]
    [Route("[area]")]
    public class ClientsController : Controller
    {
        private readonly IPersonManager _personManager;
        private readonly PersonModelManager _personModelManager;
        private readonly IRoleManager _roleManager;
        private readonly IWorkshopManager _workshopManager;

        public ClientsController(IPersonManager personManager,
            PersonModelManager personModelManager,
            IRoleManager roleManager,
            IWorkshopManager workshopManager)
        {
            _personManager = personManager;
            _personModelManager = personModelManager;
            _roleManager = roleManager;
            _workshopManager = workshopManager;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _personManager.GetCustomers());
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            ViewData["Workshops"] = await _workshopManager.GetAsync();
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(PersonModel model)
        {
            model.UserName = model.Email;
            await _personModelManager.CreateAsync(model);
            model = await _personModelManager.FindByEmailAsync(model.Email);
            await _roleManager.AddToRole(model.Id, _roleManager.Customer.Id);
            return RedirectToAction("Index");
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            ViewData["Workshops"] = await _workshopManager.GetAsync();
            return View(await _personManager.GetAsync(id));
        }

        [HttpPost("edit")]
        public async Task<IActionResult> Update(PersonModel model)
        {
            var personModel = await _personManager.GetAsync(model.Id);
            PersonMapping(model, personModel);
            await _personModelManager.UpdateAsync(personModel);
            return RedirectToAction("Index");
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _personManager.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        private void PersonMapping(PersonModel newModel, PersonModel oldModel)
        {
            oldModel.FirstName = newModel.FirstName;
            oldModel.LastName = newModel.LastName;
            oldModel.Email = newModel.Email;
            oldModel.Birthday = newModel.Birthday;
            oldModel.UserName = newModel.Email;
        }
    }
}