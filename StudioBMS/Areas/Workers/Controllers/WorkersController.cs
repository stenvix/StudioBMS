using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudioBMS.Business.Managers.Models.Interfaces;

namespace StudioBMS.Areas.Workers.Controllers
{
    [Area("Workers")]
    [Route("[area]")]
    public class WorkersController : Controller
    {
        private readonly IPersonManager _personManager;

        public WorkersController(IPersonManager personManager)
        {
            _personManager = personManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _personManager.GetAsync());
        }
    }
}