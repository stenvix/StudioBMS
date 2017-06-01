using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.Managers.Models.Base.Interfaces;

namespace StudioBMS.Business.Managers.Models.Interfaces
{
    public interface IServiceManager : IManager<ServiceModel>
    {
        Task<IList<ServiceModel>> FindByPerson(Guid personId);
    }
}