using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.Managers.Repositories.Interfaces;
using StudioBMS.Core.Entities;

namespace StudioBMS.Business.Managers.Identity.Stores
{
    public class PersonModelStore : IUserStore<PersonModel>
    {
        public PersonModelStore(IUnitOfWork context)
        {
            Context = context;
        }

        private IUnitOfWork Context { get; }

        public Task<string> GetUserIdAsync(PersonModel user, CancellationToken cancellationToken)
        {
            return Context.PersonStore.GetUserIdAsync(Mapper.Map<PersonModel, Person>(user), cancellationToken);
        }

        public Task<string> GetUserNameAsync(PersonModel user, CancellationToken cancellationToken)
        {
            return Context.PersonStore.GetUserNameAsync(Mapper.Map<PersonModel, Person>(user), cancellationToken);
        }

        public Task SetUserNameAsync(PersonModel user, string userName, CancellationToken cancellationToken)
        {
            return Context.PersonStore.SetUserNameAsync(Mapper.Map<PersonModel, Person>(user), userName, cancellationToken);
        }

        public Task<string> GetNormalizedUserNameAsync(PersonModel user, CancellationToken cancellationToken)
        {
            return Context.PersonStore.GetNormalizedUserNameAsync(Mapper.Map<PersonModel, Person>(user), cancellationToken);
        }

        public Task SetNormalizedUserNameAsync(PersonModel user, string normalizedName,
            CancellationToken cancellationToken)
        {
            return Context.PersonStore.SetNormalizedUserNameAsync(Mapper.Map<PersonModel, Person>(user), normalizedName, cancellationToken);
        }

        public Task<IdentityResult> CreateAsync(PersonModel user, CancellationToken cancellationToken)
        {
            return Context.PersonStore.CreateAsync(Mapper.Map<PersonModel, Person>(user), cancellationToken);
        }

        public Task<IdentityResult> UpdateAsync(PersonModel user, CancellationToken cancellationToken)
        {
            return Context.PersonStore.UpdateAsync(Mapper.Map<PersonModel, Person>(user), cancellationToken);
        }

        public Task<IdentityResult> DeleteAsync(PersonModel user, CancellationToken cancellationToken)
        {
            return Context.PersonStore.DeleteAsync(Mapper.Map<PersonModel, Person>(user), cancellationToken);
        }

        public async Task<PersonModel> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return  Mapper.Map<Person,PersonModel>(await Context.PersonStore.FindByIdAsync(userId, cancellationToken));
        }

        public async Task<PersonModel> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return Mapper.Map<Person, PersonModel>(await Context.PersonStore.FindByNameAsync(normalizedUserName, cancellationToken));
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}