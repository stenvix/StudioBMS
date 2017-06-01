using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudioBMS.Business.Managers.Models.Interfaces;

namespace StudioBMS.Areas.Statistics.Controllers
{
    [Area("Statistics"), Route("[area]")]
    public class StatisticsController : Controller
    {
        private readonly IWorkshopManager _workshopManager;

        public StatisticsController(IWorkshopManager workshopManager)
        {
            _workshopManager = workshopManager;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["Workshops"] = await _workshopManager.GetAsync();
            return View();
        }
    }
}