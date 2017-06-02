using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.Managers.Models.Interfaces;

namespace StudioBMS.Areas.Services.Controllers
{
    [Area("Services"), Route("[area]")]
    public class ServicesController : Controller
    {
        private readonly IServiceManager _serviceManager;
        public ServicesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _serviceManager.GetAsync());
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(ServiceModel model)
        {
            await _serviceManager.CreateAsync(model);
            return RedirectToAction("Index");
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            //TODO: Add message
            var model = await _serviceManager.GetAsync(id);
            return View(model);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> Update(ServiceModel model)
        {
            //TODO: Add message
            await _serviceManager.UpdateAsync(model);
            return RedirectToAction("Index");
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _serviceManager.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}