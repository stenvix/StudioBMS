using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudioBMS.Business.Managers.Models.Interfaces;

namespace StudioBMS.Areas.Clients.Controllers
{
    [Area("Clients")]
    [Route("[area]")]
    public class ClientsController : Controller
    {
        private readonly IPersonManager _personManager;

        public ClientsController(IPersonManager personManager)
        {
            _personManager = personManager;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _personManager.GetClients());
        }
    }
}