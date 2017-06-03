using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.Managers.Models.Base.Interfaces;

namespace StudioBMS.Business.Managers.Models.Interfaces
{
    public interface IRoleManager : IManager<RoleModel>
    {
        RoleModel Customer { get; }
        Task<IList<RoleModel>> GetWorkerRoles();
        Task ClearRoles(Guid personId);
        Task AddToRole(Guid personId, Guid roleId);
    }
}