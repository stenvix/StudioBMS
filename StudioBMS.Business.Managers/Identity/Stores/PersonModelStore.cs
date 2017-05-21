using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudioBMS.Business.DTO.Extensions;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.Managers.Repositories.Interfaces;
using StudioBMS.Core.Entities;

namespace StudioBMS.Business.Managers.Identity.Stores
{
    public class PersonModelStore : IUserPasswordStore<PersonModel>, IUserPhoneNumberStore<PersonModel>,
        IUserTwoFactorStore<PersonModel>,
        IUserLoginStore<PersonModel> // IUserRoleStore<PersonModel>, IUserClaimStore<PersonModel>, IUserSecurityStampStore<PersonModel>, IUserEmailStore<PersonModel>, IUserLockoutStore<PersonModel>, IUserPhoneNumberStore<PersonModel>, IQueryableUserStore<PersonModel>, IUserTwoFactorStore<PersonModel>, IUserAuthenticationTokenStore<PersonModel>
    {
        public PersonModelStore(IUnitOfWork context)
        {
            Context = context;
        }

        private IUnitOfWork Context { get; }

        public Task AddLoginAsync(PersonModel user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            return Context.PersonStore.AddLoginAsync(user.To<Person>(), login, cancellationToken);
        }

        public Task RemoveLoginAsync(PersonModel user, string loginProvider, string providerKey,
            CancellationToken cancellationToken)
        {
            return Context.PersonStore.RemoveLoginAsync(user.To<Person>(), loginProvider, providerKey,
                cancellationToken);
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(PersonModel user, CancellationToken cancellationToken)
        {
            return Context.PersonStore.GetLoginsAsync(user.To<Person>(), cancellationToken);
        }

        public async Task<PersonModel> FindByLoginAsync(string loginProvider, string providerKey,
            CancellationToken cancellationToken)
        {
            return Mapper.Map<Person, PersonModel>(
                await Context.PersonStore.FindByLoginAsync(loginProvider, providerKey, cancellationToken));
        }

        public Task<string> GetUserIdAsync(PersonModel user, CancellationToken cancellationToken)
        {
            return Context.PersonStore.GetUserIdAsync(user.To<Person>(), cancellationToken);
        }

        public Task<string> GetUserNameAsync(PersonModel user, CancellationToken cancellationToken)
        {
            return Context.PersonStore.GetUserNameAsync(user.To<Person>(), cancellationToken);
        }

        public Task SetUserNameAsync(PersonModel user, string userName, CancellationToken cancellationToken)
        {
            return Context.PersonStore.SetUserNameAsync(user.To<Person>(), userName, cancellationToken);
        }

        public Task<string> GetNormalizedUserNameAsync(PersonModel user, CancellationToken cancellationToken)
        {
            return Context.PersonStore.GetNormalizedUserNameAsync(Mapper.Map<PersonModel, Person>(user),
                cancellationToken);
        }

        public Task SetNormalizedUserNameAsync(PersonModel user, string normalizedName,
            CancellationToken cancellationToken)
        {
            return Context.PersonStore.SetNormalizedUserNameAsync(Mapper.Map<PersonModel, Person>(user), normalizedName,
                cancellationToken);
        }

        public Task<IdentityResult> CreateAsync(PersonModel user, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = user.UserName.ToUpper();
            user.NormalizedEmail = user.Email.ToUpper();
            return Context.PersonStore.CreateAsync(user.To<Person>(), cancellationToken);
        }

        public Task<IdentityResult> UpdateAsync(PersonModel user, CancellationToken cancellationToken)
        {
            return Context.PersonStore.UpdateAsync(user.To<Person>(), cancellationToken);
        }

        public Task<IdentityResult> DeleteAsync(PersonModel user, CancellationToken cancellationToken)
        {
            return Context.PersonStore.DeleteAsync(user.To<Person>(), cancellationToken);
        }

        public async Task<PersonModel> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return Mapper.Map<Person, PersonModel>(await Context.PersonStore.FindByIdAsync(userId, cancellationToken));
        }

        public async Task<PersonModel> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var result =
                Mapper.Map<Person, PersonModel>(
                    await Context.PersonStore.FindByNameAsync(normalizedUserName, cancellationToken));
            return result;
        }

        public async Task SetPasswordHashAsync(PersonModel user, string passwordHash,
            CancellationToken cancellationToken)
        {
            var userEntity = user.To<Person>();
            await Context.PersonStore.SetPasswordHashAsync(userEntity, passwordHash, cancellationToken);
            Mapper.Map(userEntity, user);
        }

        public Task<string> GetPasswordHashAsync(PersonModel user, CancellationToken cancellationToken)
        {
            return Context.PersonStore.GetPasswordHashAsync(user.To<Person>(), cancellationToken);
        }

        public Task<bool> HasPasswordAsync(PersonModel user, CancellationToken cancellationToken)
        {
            return Context.PersonStore.HasPasswordAsync(user.To<Person>(), cancellationToken);
        }

        public void Dispose()
        {
            Context?.Dispose();
        }

        public async Task SetPhoneNumberAsync(PersonModel user, string phoneNumber, CancellationToken cancellationToken)
        {
            var userEntity = user.To<Person>();
            await Context.PersonStore.SetPhoneNumberAsync(user.To<Person>(), phoneNumber, cancellationToken);
            Mapper.Map(userEntity, user);
        }

        public Task<string> GetPhoneNumberAsync(PersonModel user, CancellationToken cancellationToken)
        {
            return Context.PersonStore.GetPhoneNumberAsync(user.To<Person>(), cancellationToken);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(PersonModel user, CancellationToken cancellationToken)
        {
            return Context.PersonStore.GetPhoneNumberConfirmedAsync(user.To<Person>(), cancellationToken);
        }

        public async Task SetPhoneNumberConfirmedAsync(PersonModel user, bool confirmed,
            CancellationToken cancellationToken)
        {
            var userEntity = user.To<Person>();
            await Context.PersonStore.SetPhoneNumberConfirmedAsync(user.To<Person>(), confirmed, cancellationToken);
            Mapper.Map(userEntity, user);
        }

        public async Task SetTwoFactorEnabledAsync(PersonModel user, bool enabled, CancellationToken cancellationToken)
        {
            var userEntity = user.To<Person>();
            await Context.PersonStore.SetTwoFactorEnabledAsync(user.To<Person>(), enabled, cancellationToken);
            Mapper.Map(userEntity, user);
        }

        public Task<bool> GetTwoFactorEnabledAsync(PersonModel user, CancellationToken cancellationToken)
        {
            return Context.PersonStore.GetTwoFactorEnabledAsync(user.To<Person>(), cancellationToken);
        }
    }
}