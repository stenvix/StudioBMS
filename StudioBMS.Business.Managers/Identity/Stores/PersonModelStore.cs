using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using StudioBMS.Business.DTO.Extensions;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.Managers.Repositories.Interfaces;
using StudioBMS.Core.Entities;

namespace StudioBMS.Business.Managers.Identity.Stores
{
    public class PersonModelStore : IUserPasswordStore<PersonModel>, IUserPhoneNumberStore<PersonModel>,
        IUserTwoFactorStore<PersonModel>,
        IUserLoginStore<PersonModel>, IUserEmailStore<PersonModel>, IUserRoleStore<PersonModel> //  IUserClaimStore<PersonModel>, IUserSecurityStampStore<PersonModel>, IUserLockoutStore<PersonModel>, IUserPhoneNumberStore<PersonModel>, IQueryableUserStore<PersonModel>, IUserTwoFactorStore<PersonModel>, IUserAuthenticationTokenStore<PersonModel>
    {
        public PersonModelStore(IUnitOfWork context)
        {
            Context = context;
        }

        private IUnitOfWork Context { get; }

        public Task AddLoginAsync(PersonModel user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            return Context.PersonStoreStore.AddLoginAsync(user.To<Person>(), login, cancellationToken);
        }

        public Task RemoveLoginAsync(PersonModel user, string loginProvider, string providerKey,
            CancellationToken cancellationToken)
        {
            return Context.PersonStoreStore.RemoveLoginAsync(user.To<Person>(), loginProvider, providerKey,
                cancellationToken);
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(PersonModel user, CancellationToken cancellationToken)
        {
            return Context.PersonStoreStore.GetLoginsAsync(user.To<Person>(), cancellationToken);
        }

        public async Task<PersonModel> FindByLoginAsync(string loginProvider, string providerKey,
            CancellationToken cancellationToken)
        {
            return Mapper.Map<Person, PersonModel>(
                await Context.PersonStoreStore.FindByLoginAsync(loginProvider, providerKey, cancellationToken));
        }

        public Task<string> GetUserIdAsync(PersonModel user, CancellationToken cancellationToken)
        {
            return Context.PersonStoreStore.GetUserIdAsync(user.To<Person>(), cancellationToken);
        }

        public Task<string> GetUserNameAsync(PersonModel user, CancellationToken cancellationToken)
        {
            return Context.PersonStoreStore.GetUserNameAsync(user.To<Person>(), cancellationToken);
        }

        public Task SetUserNameAsync(PersonModel user, string userName, CancellationToken cancellationToken)
        {
            return Context.PersonStoreStore.SetUserNameAsync(user.To<Person>(), userName, cancellationToken);
        }

        public Task<string> GetNormalizedUserNameAsync(PersonModel user, CancellationToken cancellationToken)
        {
            return Context.PersonStoreStore.GetNormalizedUserNameAsync(Mapper.Map<PersonModel, Person>(user),
                cancellationToken);
        }

        public Task SetNormalizedUserNameAsync(PersonModel user, string normalizedName,
            CancellationToken cancellationToken)
        {
            return Context.PersonStoreStore.SetNormalizedUserNameAsync(Mapper.Map<PersonModel, Person>(user), normalizedName,
                cancellationToken);
        }

        public Task<IdentityResult> CreateAsync(PersonModel user, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = user.UserName.ToUpper();
            user.NormalizedEmail = user.Email.ToUpper();
            return Context.PersonStoreStore.CreateAsync(user.To<Person>(), cancellationToken);
        }

        public Task<IdentityResult> UpdateAsync(PersonModel user, CancellationToken cancellationToken)
        {
            return Context.PersonStoreStore.UpdateAsync(user.To<Person>(), cancellationToken);
        }

        public Task<IdentityResult> DeleteAsync(PersonModel user, CancellationToken cancellationToken)
        {
            return Context.PersonStoreStore.DeleteAsync(user.To<Person>(), cancellationToken);
        }

        public async Task<PersonModel> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return Mapper.Map<Person, PersonModel>(await Context.PersonStoreStore.FindByIdAsync(userId, cancellationToken));
        }

        public async Task<PersonModel> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var result =
                Mapper.Map<Person, PersonModel>(
                    await Context.PersonStoreStore.FindByNameAsync(normalizedUserName, cancellationToken));
            return result;
        }

        public async Task SetPasswordHashAsync(PersonModel user, string passwordHash,
            CancellationToken cancellationToken)
        {
            var userEntity = user.To<Person>();
            await Context.PersonStoreStore.SetPasswordHashAsync(userEntity, passwordHash, cancellationToken);
            user.PasswordHash = userEntity.PasswordHash;
        }

        public Task<string> GetPasswordHashAsync(PersonModel user, CancellationToken cancellationToken)
        {
            return Context.PersonStoreStore.GetPasswordHashAsync(user.To<Person>(), cancellationToken);
        }

        public Task<bool> HasPasswordAsync(PersonModel user, CancellationToken cancellationToken)
        {
            return Context.PersonStoreStore.HasPasswordAsync(user.To<Person>(), cancellationToken);
        }

        public void Dispose()
        {
            Context?.Dispose();
        }

        public async Task SetPhoneNumberAsync(PersonModel user, string phoneNumber, CancellationToken cancellationToken)
        {
            var userEntity = user.To<Person>();
            await Context.PersonStoreStore.SetPhoneNumberAsync(user.To<Person>(), phoneNumber, cancellationToken);
            user.PhoneNumber = userEntity.PhoneNumber;
        }

        public Task<string> GetPhoneNumberAsync(PersonModel user, CancellationToken cancellationToken)
        {
            return Context.PersonStoreStore.GetPhoneNumberAsync(user.To<Person>(), cancellationToken);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(PersonModel user, CancellationToken cancellationToken)
        {
            return Context.PersonStoreStore.GetPhoneNumberConfirmedAsync(user.To<Person>(), cancellationToken);
        }

        public async Task SetPhoneNumberConfirmedAsync(PersonModel user, bool confirmed,
            CancellationToken cancellationToken)
        {
            var userEntity = user.To<Person>();
            await Context.PersonStoreStore.SetPhoneNumberConfirmedAsync(user.To<Person>(), confirmed, cancellationToken);
            user.PhoneNumberConfirmed = userEntity.PhoneNumberConfirmed;
        }

        public async Task SetTwoFactorEnabledAsync(PersonModel user, bool enabled, CancellationToken cancellationToken)
        {
            var userEntity = user.To<Person>();
            await Context.PersonStoreStore.SetTwoFactorEnabledAsync(user.To<Person>(), enabled, cancellationToken);
            user.TwoFactorEnabled = userEntity.TwoFactorEnabled;
        }

        public Task<bool> GetTwoFactorEnabledAsync(PersonModel user, CancellationToken cancellationToken)
        {
            return Context.PersonStoreStore.GetTwoFactorEnabledAsync(user.To<Person>(), cancellationToken);
        }

        public async Task SetEmailAsync(PersonModel user, string email, CancellationToken cancellationToken)
        {
            var userEntity = user.To<Person>();
            await Context.PersonStoreStore.SetEmailAsync(userEntity, email, cancellationToken);
            user.Email = userEntity.Email;
        }

        public Task<string> GetEmailAsync(PersonModel user, CancellationToken cancellationToken)
        {
            return Context.PersonStoreStore.GetEmailAsync(user.To<Person>(), cancellationToken);
        }

        public Task<bool> GetEmailConfirmedAsync(PersonModel user, CancellationToken cancellationToken)
        {
            return Context.PersonStoreStore.GetEmailConfirmedAsync(user.To<Person>(), cancellationToken);
        }

        public async Task SetEmailConfirmedAsync(PersonModel user, bool confirmed, CancellationToken cancellationToken)
        {
            var userEntity = user.To<Person>();
            await Context.PersonStoreStore.SetEmailConfirmedAsync(userEntity, confirmed, cancellationToken);
            Mapper.Map(userEntity, user);
        }

        public async Task<PersonModel> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            return (await Context.PersonStoreStore.FindByEmailAsync(normalizedEmail, cancellationToken)).To<PersonModel>();
        }

        public Task<string> GetNormalizedEmailAsync(PersonModel user, CancellationToken cancellationToken)
        {
            return Context.PersonStoreStore.GetNormalizedEmailAsync(user.To<Person>(), cancellationToken);
        }

        public async Task SetNormalizedEmailAsync(PersonModel user, string normalizedEmail, CancellationToken cancellationToken)
        {
            var userEntity = user.To<Person>();
            await Context.PersonStoreStore.SetNormalizedEmailAsync(userEntity, normalizedEmail, cancellationToken);
            user.NormalizedEmail = userEntity.NormalizedEmail;
        }

        public Task AddToRoleAsync(PersonModel user, string roleName, CancellationToken cancellationToken)
        {
            return Context.PersonStoreStore.AddToRoleAsync(user.To<Person>(), roleName, cancellationToken);
        }

        public Task RemoveFromRoleAsync(PersonModel user, string roleName, CancellationToken cancellationToken)
        {
            return Context.PersonStoreStore.RemoveFromRoleAsync(user.To<Person>(), roleName, cancellationToken);
        }

        public Task<IList<string>> GetRolesAsync(PersonModel user, CancellationToken cancellationToken)
        {
            return Context.PersonStoreStore.GetRolesAsync(user.To<Person>(), cancellationToken);
        }

        public Task<bool> IsInRoleAsync(PersonModel user, string roleName, CancellationToken cancellationToken)
        {
            return Context.PersonStoreStore.IsInRoleAsync(user.To<Person>(), roleName, cancellationToken);
        }

        public async Task<IList<PersonModel>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            return Mapper.Map<IList<Person>, IList<PersonModel>>(await Context.PersonStoreStore.GetUsersInRoleAsync(roleName.Normalize(), cancellationToken));
        }
    }
}