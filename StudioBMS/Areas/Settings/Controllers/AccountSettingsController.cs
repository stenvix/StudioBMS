using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.Managers.Identity;
using StudioBMS.Business.Managers.Models.Impl;
using StudioBMS.Business.Managers.Models.Interfaces;
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

        private readonly Dictionary<MessageType, string> _messages = new Dictionary<MessageType, string>
        {
            {MessageType.Success, "PasswordUpdated"}
        };

        private readonly PersonModelSignInManager _signInManager;
        private readonly IPersonManager _personManager;
        private readonly PersonModelManager _userManager;

        public AccountSettingsController(
            PersonModelManager userManager,
            PersonModelSignInManager signInManager,
            IPersonManager personManager,
            IOptions<IdentityCookieOptions> identityCookieOptions,
            IEmailSender emailSender,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _personManager = personManager;
            _externalCookieScheme = identityCookieOptions.Value.ExternalCookieAuthenticationScheme;
            _emailSender = emailSender;
            _logger = loggerFactory.CreateLogger<ManageController>();
        }

        [HttpGet]
        public async Task<IActionResult> Index(MessageType? message)
        {
            if (message.HasValue)
                ViewData["Message"] =
                    new MessageViewModel { Message = _messages[message.Value], Type = MessageType.Success };
            return View(await _userManager.FindByEmailAsync(User.Identity.Name));
        }

        [HttpPost]
        public async Task<IActionResult> Update(PersonModel model)
        {
            var entity = await _personManager.GetAsync(model.Id);
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;

            if (entity.UserName != model.UserName)
            {
                entity.Email = model.UserName;
                entity.UserName = model.UserName;
                entity.NormalizedEmail = model.UserName.ToUpper().Normalize();
                entity.NormalizedUserName = model.UserName.ToUpper().Normalize();
                await _signInManager.SignOutAsync();
            }
            entity.Language = model.Language;
            entity.Birthday = model.Birthday;
            entity.PhoneNumber = model.PhoneNumber;
            await _personManager.UpdateAsync(entity);
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                $"c={model.Language}|uic={model.Language}");
            return RedirectToAction("Index");
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
                    return RedirectToAction(nameof(Index), new { Message = MessageType.Success });
                }
                AddErrors(result);
                return View(model);
            }
            return RedirectToAction(nameof(Index), new { Message = MessageType.Danger });
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