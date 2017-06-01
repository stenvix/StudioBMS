using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudioBMS.Business.Interfaces.Repositories.Base;
using StudioBMS.Core.Entities;

namespace StudioBMS.Repositories.Interfaces
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<IEnumerable<Person>> FindByRole(Guid roleId, Guid[] excluded = default(Guid[]));
        Task<IEnumerable<Person>> FindByWorkshopAndNotInRoles(Guid[] excluded, Guid workshopId = default(Guid));
        Task<Person> FindByName(string username);
        Task<IQueryable<Person>> GetWithTimetables();
    }
}