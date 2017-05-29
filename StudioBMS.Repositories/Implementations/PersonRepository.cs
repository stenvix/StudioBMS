using System;
using System.Collections.Generic;
using System.Linq;
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

        protected override IEnumerable<Person> Include()
        {
            return Set.Include(i => i.Roles).ThenInclude(i => i.Role).Include(i => i.PersonTimetables).ThenInclude(i => i.Timetable);
        }

        public Task<IEnumerable<Person>> FindByRole(Guid roleId, Guid[] excluded = default(Guid[]))
        {
            if (roleId != Guid.Empty)
            {
                return Task.Run(() => Include().Where(i => i.Roles.Any(r => r.RoleId == roleId)));
            }

            if(excluded == null)
                throw new ArgumentException("You need to provide excluded roles");

            return Task.Run(() => Include().Where(i => i.Roles.Any(r => !excluded.Any(e => e == r.RoleId))));
        }

        public async Task<IEnumerable<Person>> FindByWorkshopAndNotInRoles(Guid? workshopId, Guid[] excluded)
        {
            return (await FindByRole(Guid.Empty, excluded)).Where(i=> !workshopId.HasValue || i.WorkshopId == workshopId);
        }
    }
}