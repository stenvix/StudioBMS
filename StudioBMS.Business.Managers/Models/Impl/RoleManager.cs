using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using StudioBMS.Business.DTO.Extensions;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.Managers.Models.Base.Impl;
using StudioBMS.Business.Managers.Models.Interfaces;
using StudioBMS.Business.Managers.Repositories.Interfaces;
using StudioBMS.Core.Entities.IdentityBase;

namespace StudioBMS.Business.Managers.Models.Impl
{
    public class RoleManager : CrudManager<RoleModel, Role>, IRoleManager
    {
        public RoleManager(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.RoleRepository)
        {
        }

        public RoleModel Customer => _unitOfWork.RoleRepository.Customer.To<RoleModel>();

        public async Task<IList<RoleModel>> GetWorkerRoles()
        {
            var result = await _unitOfWork.RoleRepository.GetWorkerRoles();
            return result.To<RoleModel>();
        }

        public async Task ClearRoles(Guid personId)
        {
            await _unitOfWork.RoleRepository.ClearRoles(personId);
            await _unitOfWork.SaveChanges();
        }

        public async Task AddToRole(Guid personId, Guid roleId)
        {
            await _unitOfWork.RoleRepository.AddToRole(personId, roleId);
            await _unitOfWork.SaveChanges();
        }
    }
}