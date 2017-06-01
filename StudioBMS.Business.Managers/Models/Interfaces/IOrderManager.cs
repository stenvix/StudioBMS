using System.Collections.Generic;
using System.Threading.Tasks;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.Managers.Models.Base.Interfaces;

namespace StudioBMS.Business.Managers.Models.Interfaces
{
    public interface IOrderManager : IManager<OrderModel>
    {
        Task<OrderModel> CreateAsync(OrderViewModel item);
        Task<IList<OrderStatusModel>> GetStatuses();
    }
}