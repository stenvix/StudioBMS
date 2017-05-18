using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StudioBMS.Business.DTO.Models;

namespace StudioBMS.Business.Managers.Identity
{
    public class PersonModelSignInManager : SignInManager<PersonModel>
    {
        public PersonModelSignInManager(UserManager<PersonModel> userManager, IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<PersonModel> claimsFactory, IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<PersonModel>> logger) : base(userManager, contextAccessor, claimsFactory,
            optionsAccessor, logger)
        {
        }
    }
}