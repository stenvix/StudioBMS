using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudioBMS.Business.Interfaces.Repositories.Base;
using StudioBMS.Core.Entities.IdentityBase;

namespace StudioBMS.Repositories.Interfaces
{
    public interface IRoleRepository : IRepository<Role>
    {
        Role Customer { get; }
        Role Administrator { get; }
        Role Manager { get; }
        Task<IEnumerable<Role>> GetWorkerRoles();
        Task ClearRoles(Guid personId);
        Task AddToRole(Guid personId, Guid roleId);
    }
}