using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using StudioBMS.Core.Entities;
using StudioBMS.Core.Entities.Base;
using StudioBMS.Core.Entities.IdentityBase;
using StudioBMS.Database.Context;
using StudioBMS.Repositories.Interfaces;

namespace StudioBMS.Repositories.Implementations
{
    public class PersonRepository : UserStore<Person, Role, StudioContext, Guid, PersonClaim, PersonRole, PersonLogin,
        PersonToken, RoleClaim>, IPersonRepository
    {
        public PersonRepository(StudioContext context, IdentityErrorDescriber describer = null) : base(context,
            describer)
        {
        }

        public Task<IQueryable<Person>> GetAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<Person> GetAsync(Guid id, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<Person> CreateAsync(Person entity, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<Person> Update(Person entity, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task Delete(Person entity, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
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
            return new PersonToken {UserId = user.Id, LoginProvider = loginProvider, Name = name, Value = value};
        }
    }
}