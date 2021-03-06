﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudioBMS.Core.Entities;
using StudioBMS.Database.Context;
using StudioBMS.Repositories.Implementations.Base;
using StudioBMS.Repositories.Interfaces;

namespace StudioBMS.Repositories.Implementations
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(StudioContext context) : base(context)
        {
        }

        protected override IQueryable<Person> Include()
        {
            return Set
                .Where(i=>i.IsActive)
                .Include(i => i.Roles)
                    .ThenInclude(i => i.Role)
                .Include(i => i.Workshop);
        }

        private IQueryable<Person> WithTimetables(IQueryable<Person> set)
        {
            return set.Include(i => i.PersonTimetables).ThenInclude(i => i.Timetable);
        }

        private IQueryable<Person> WithServices(IQueryable<Person> set)
        {
            return set.Include(i => i.PersonServices).ThenInclude(i => i.Service);
        }

        public Task<Person> FindByEmail(string email)
        {
            return Task.Run(() => Include().FirstOrDefault(i => i.NormalizedEmail == email.ToUpper().Normalize()));
        }

        public Task<IQueryable<Person>> GetWithTimetables()
        {
            return Task.Run(() => WithTimetables(Include()));
        }

        public Task<IEnumerable<Person>> FindByRole(Guid roleId, Guid[] excluded = default(Guid[]))
        {
            if (roleId != Guid.Empty)
            {
                return Task.Run(() => Include().Where(i => i.Roles.Any(r => r.RoleId == roleId)).AsEnumerable());
            }

            if (excluded == null)
                throw new ArgumentException("You need to provide excluded roles");

            return Task.Run(() => Include().Where(i => i.Roles.Any(r => !excluded.Any(e => e == r.RoleId))).AsEnumerable());
        }

        public async Task<IEnumerable<Person>> FindByWorkshopAndNotInRoles(Guid[] excluded, Guid workshopId = default(Guid))
        {
            return (await FindByRole(Guid.Empty, excluded)).Where(i => !(workshopId != default(Guid)) || i.WorkshopId == workshopId);
        }

        public Task<Person> FindByName(string username)
        {
            var normalizedUsername = username.ToUpper().Normalize();
            return Task.Run(() => Include().FirstOrDefault(i => i.UserName.ToUpper().Normalize() == normalizedUsername));
        }

        public override Task DeleteAsync(Person entity, CancellationToken cancellationToken = new CancellationToken())
        {
            entity.IsActive = false;
            var guid = Guid.NewGuid();
            entity.Email = guid.ToString();
            entity.NormalizedEmail = guid.ToString();
            entity.UserName = guid.ToString();
            entity.NormalizedUserName = guid.ToString();
            entity.PhoneNumber = string.Empty;

            foreach (var personService in Context.PersonServices.Where(i=>i.PersonId == entity.Id))
            {
                Context.PersonServices.Remove(personService);
            }
            foreach (var personTimetable in Context.PersonTimetables.Where(i=>i.PersonId == entity.Id))
            {
                Context.PersonTimetables.Remove(personTimetable);
            }
            return Update(entity, cancellationToken);
        }
    }
}