using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.Managers.Identity;
using StudioBMS.Controllers;
using StudioBMS.Models.ManageViewModels;
using StudioBMS.Models.UI;
using StudioBMS.Services;

namespace StudioBMS.Areas.Settings.Controllers
{
    [Area("Settings")]
    [Route("[area]")]
    public class AccountSettingsController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly string _externalCookieScheme;
        private readonly ILogger _logger;
        private readonly PersonModelSignInManager _signInManager;
        private readonly PersonModelManager _userManager;

        public AccountSettingsController(
            PersonModelManager userManager,
            PersonModelSignInManager signInManager,
            IOptions<IdentityCookieOptions> identityCookieOptions,
            IEmailSender emailSender,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _externalCookieScheme = identityCookieOptions.Value.ExternalCookieAuthenticationScheme;
            _emailSender = emailSender;
            _logger = loggerFactory.CreateLogger<ManageController>();
        }
        private readonly Dictionary<MessageType, string> _messages =new Dictionary<MessageType, string>
        {
            {MessageType.Success, "PasswordUpdated"}
        };

        [HttpGet]
        public IActionResult Index(MessageType? message)
        {
            if (message.HasValue)
            {
                ViewData["Message"] =  new MessageViewModel{Message = _messages[message.Value], Type = MessageType.Success};
            }
            return View();
        }

        [HttpGet("password")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost("password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    _logger.LogInformation(3, "User changed their password successfully.");
                    return RedirectToAction(nameof(Index), new {Message = MessageType.Success});
                }
                AddErrors(result);
                return View(model);
            }
            return RedirectToAction(nameof(Index), new { Message = MessageType.Danger});
        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
        }
        private Task<PersonModel> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }


    }
}