using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.Managers.Models.Base.Impl;
using StudioBMS.Business.Managers.Models.Interfaces;
using StudioBMS.Business.Managers.Repositories.Interfaces;
using StudioBMS.Core.Entities;
using AutoMapper;

namespace StudioBMS.Business.Managers.Models.Impl
{
    public class ServiceManager : CrudManager<ServiceModel, Service>, IServiceManager
    {
        public ServiceManager(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.ServiceRepository)
        {
        }

        public async Task<IList<ServiceModel>> FindByPerson(Guid personId)
        {
            var result = await _unitOfWork.ServiceRepository.FindByPerson(personId);
            return Mapper.Map<IEnumerable<Service>, IList<ServiceModel>>(result);
        }

        public async Task CreatePerson(Guid personId, Guid serviceId)
        {
            await _unitOfWork.ServiceRepository.CreatePerson(personId, serviceId);
            await _unitOfWork.SaveChanges();
        }

        public async Task DeletePersonService(Guid personId, Guid serviceId)
        {
            await _unitOfWork.ServiceRepository.DeletePersonService(personId, serviceId);
            await _unitOfWork.SaveChanges();
        }
    }
}