using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.Managers.Models.Base.Interfaces;

namespace StudioBMS.Business.Managers.Models.Interfaces
{
    public interface IPersonManager : IManager<PersonModel>
    {
        Task<PersonModel> FindByEmail(string email);
        Task<IList<PersonModel>> GetCustomers();
        Task<IList<PersonModel>> GetEmployees(Guid workshopId = default(Guid));
        Task<IList<PersonModel>> GetStaff();
        Task<IList<PersonModel>> GetWithPerformerOrders(Guid[] ids, DateTime date = default(DateTime), bool isWorker = false);
        Task<PersonModel> FindByName(string username);
        Task<bool> IsInRole(Guid personId, string roleName);
    }
}