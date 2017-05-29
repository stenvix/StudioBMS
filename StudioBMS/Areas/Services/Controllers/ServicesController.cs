using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
    }
}