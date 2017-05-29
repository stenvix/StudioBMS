using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.Managers.Identity;
using StudioBMS.Business.Managers.Models.Interfaces;

namespace StudioBMS.Areas.Workers.Controllers
{
    [Area("Workers")]
    [Route("[area]")]
    public class WorkersController : Controller
    {
        private readonly IPersonManager _personManager;
        private readonly PersonModelManager _permonModelManager;

        public WorkersController(IPersonManager personManager, PersonModelManager permonModelManager)
        {
            _personManager = personManager;
            _permonModelManager = permonModelManager;
        }

        public async Task<IActionResult> Index()
        {
            IList<PersonModel> employees = null;
            if (User.IsInRole("Administrator"))
            {
                employees = await _personManager.GetStaff();
            }
            else
            {
                var manager = await _permonModelManager.FindByNameAsync(User.Identity.Name);
                employees = await _personManager.GetEmployees(manager.Workshop.Id);
            }
            return View(employees);
        }
    }
}