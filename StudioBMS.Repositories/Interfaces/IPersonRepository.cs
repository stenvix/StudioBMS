using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudioBMS.Business.Interfaces.Repositories.Base;
using StudioBMS.Core.Entities;

namespace StudioBMS.Repositories.Interfaces
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<IEnumerable<Person>> FindByRole(Guid roleId, Guid[] excluded = default(Guid[]));
        Task<IEnumerable<Person>> FindByWorkshopAndNotInRoles(Guid? workshopId, Guid[] excluded);
    }
}