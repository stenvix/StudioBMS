using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudioBMS.Core.Entities;
using StudioBMS.Core.Entities.IdentityBase;
using StudioBMS.Database.Context;

namespace StudioBMS.Repositories.Implementations
{
    public class PersonStoreRepository : UserStore<Person, Role, StudioContext, Guid, PersonClaim, PersonRole,
        PersonLogin,
        PersonToken, RoleClaim>
    {
        public PersonStoreRepository(StudioContext context, IdentityErrorDescriber describer = null) : base(context,
            describer)
        {
        }

        public override Task<IdentityResult> UpdateAsync(Person user, CancellationToken cancellationToken = new CancellationToken())
        {
            var local = Context.Users.Local.First(i => i.Id == user.Id);
            if (local != null)
            {
                var entry = Context.Entry(local);
                entry.State = EntityState.Detached;
            }
            return base.UpdateAsync(user, cancellationToken);
        }

        protected override PersonRole CreateUserRole(Person user, Role role)
        {
            return new PersonRole {UserId = user.Id, RoleId = role.Id};
        }

        protected override PersonClaim CreateUserClaim(Person user, Claim claim)
        {
            return new PersonClaim {UserId = user.Id, ClaimType = claim.Type, ClaimValue = claim.Value};
        }

        protected override PersonLogin CreateUserLogin(Person user, UserLoginInfo login)
        {
            return new PersonLogin
            {
                UserId = user.Id,
                LoginProvider = login.LoginProvider,
                ProviderDisplayName = login.ProviderDisplayName,
                ProviderKey = login.ProviderKey
            };
        }

        protected override PersonToken CreateUserToken(Person user, string loginProvider, string name, string value)
        {
            return new PersonToken
            {
                UserId = user.Id,
                LoginProvider = loginProvider,
                Name = name,
                Value = value
            };
        }
    }
}