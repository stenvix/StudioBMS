using System.Collections.Generic;
using System.Threading.Tasks;
using StudioBMS.Business.Interfaces.Repositories.Base;
using StudioBMS.Core.Entities;
using System;

namespace StudioBMS.Repositories.Interfaces
{
    public interface IServiceRepository : IRepository<Service>
    {
        Task<IEnumerable<Service>> FindByPerson(Guid personId);
    }
}