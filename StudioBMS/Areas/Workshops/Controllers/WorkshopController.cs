using Microsoft.AspNetCore.Mvc;

namespace StudioBMS.Areas.Workshops.Controllers
{
    [Area("Workshops")]
    [Route("[area]")]
    public class WorkshopsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}