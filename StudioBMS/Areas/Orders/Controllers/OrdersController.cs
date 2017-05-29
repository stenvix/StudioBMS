using Microsoft.AspNetCore.Mvc;

namespace StudioBMS.Areas.Orders.Controllers
{
    [Area("Orders"), Route("[area]")]
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}