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
    }
}