using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.Managers.Models.Base.Interfaces;
using StudioBMS.Core.Entities.Statistics;

namespace StudioBMS.Business.Managers.Models.Interfaces
{
    public interface IOrderManager : IManager<OrderModel>
    {
        Task<OrderModel> CreateAsync(OrderViewModel item);
        Task<IList<OrderStatusModel>> GetStatuses();
        Task<IList<OrderModel>> FindByCustomer(Guid personId);
        Task<IList<OrderModel>> FindByWorkshop(Guid workshopId);
        Task ManageBalance(PaymentViewModel payment);
        Task Deactivate(Guid id);
        Task Done(Guid id);
        Task<Statistic> StatisticsByCustomer(Guid customerId, DateTime periodStart, DateTime periodEnd);
    }
}