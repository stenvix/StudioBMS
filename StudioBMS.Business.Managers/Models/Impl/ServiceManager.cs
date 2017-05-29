using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.Managers.Models.Base.Impl;
using StudioBMS.Business.Managers.Models.Interfaces;
using StudioBMS.Business.Managers.Repositories.Interfaces;
using StudioBMS.Core.Entities;

namespace StudioBMS.Business.Managers.Models.Impl
{
    public class ServiceManager : CrudManager<ServiceModel, Service>, IServiceManager
    {
        public ServiceManager(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.ServiceRepository)
        {
        }
    }
}