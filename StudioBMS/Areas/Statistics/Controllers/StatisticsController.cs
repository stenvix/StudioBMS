using Microsoft.AspNetCore.Mvc;

namespace StudioBMS.Areas.Statistics.Controllers
{
    [Area("Statistics"), Route("[area]")]
    public class StatisticsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}