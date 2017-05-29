using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["Workshops"] = await _workshopManager.GetAsync();
            ViewData["Workers"] = await _personManager.GetEmployees();
            return View();
        }
    }
}