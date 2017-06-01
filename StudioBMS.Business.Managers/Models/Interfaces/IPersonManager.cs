using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.Managers.Models.Base.Interfaces;

namespace StudioBMS.Business.Managers.Models.Interfaces
{
    public interface IPersonManager : IManager<PersonModel>
    {
        Task<IList<PersonModel>> GetClients();
        Task<IList<PersonModel>> GetEmployees(Guid workshopId = default(Guid));
        Task<IList<PersonModel>> GetStaff();
        Task<IList<PersonModel>> GetWithPerformerOrders(Guid[] ids);
        Task<PersonModel> FindByName(string username);
    }
}