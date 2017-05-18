using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StudioBMS.Business.DTO.Models;

namespace StudioBMS.Business.Managers.Identity
{
    public class PersonModelManager : UserManager<PersonModel>
    {
        public PersonModelManager(IUserStore<PersonModel> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<PersonModel> passwordHasher, IEnumerable<IUserValidator<PersonModel>> userValidators,
            IEnumerable<IPasswordValidator<PersonModel>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<PersonModel>> logger)
            : base(
                store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors,
                services, logger)
        {
        }
    }
}