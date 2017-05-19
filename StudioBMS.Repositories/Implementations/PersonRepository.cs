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
    public class PersonRepository : UserStore<Person, Role, StudioContext, Guid, PersonClaim, PersonRole, PersonLogin,
        PersonToken, RoleClaim>
    {
        public PersonRepository(StudioContext context, IdentityErrorDescriber describer = null) : base(context,
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
            var personRole = new PersonRole {UserId = user.Id, RoleId = role.Id};
            Context.UserRoles.Add(personRole);
            Context.SaveChanges();
            return personRole;
        }

        protected override PersonClaim CreateUserClaim(Person user, Claim claim)
        {
            var personClaim = new PersonClaim {UserId = user.Id, ClaimType = claim.Type, ClaimValue = claim.Value};
            Context.UserClaims.Add(personClaim);
            Context.SaveChanges();
            return personClaim;
        }

        protected override PersonLogin CreateUserLogin(Person user, UserLoginInfo login)
        {
            var personLogin = new PersonLogin
            {
                UserId = user.Id,
                LoginProvider = login.LoginProvider,
                ProviderDisplayName = login.ProviderDisplayName,
                ProviderKey = login.ProviderKey
            };
            Context.UserLogins.Add(personLogin);
            return personLogin;
        }

        protected override PersonToken CreateUserToken(Person user, string loginProvider, string name, string value)
        {
            var personToken = new PersonToken
            {
                UserId = user.Id,
                LoginProvider = loginProvider,
                Name = name,
                Value = value
            };
            Context.UserTokens.Add(personToken);
            Context.SaveChanges();
            return personToken;
        }
    }
}