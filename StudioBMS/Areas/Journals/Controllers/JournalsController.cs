using Microsoft.AspNetCore.Mvc;

namespace StudioBMS.Areas.Journals.Controllers
{
    public class JournalsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}