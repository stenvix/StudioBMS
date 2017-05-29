using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudioBMS.Business.Managers.Models.Interfaces;

namespace StudioBMS.Areas.Orders.Controllers
{
    [Area("Orders"), Route("[area]")]
    public class OrdersController : Controller
    {
        private readonly IOrderManager _orderManager;

        public OrdersController(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _orderManager.GetAsync());
        }
    }
}