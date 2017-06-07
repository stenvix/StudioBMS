using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudioBMS.Business.Interfaces.Repositories.Base;
using StudioBMS.Core.Entities;
using StudioBMS.Core.Entities.Statistics;

namespace StudioBMS.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> FindByPerformer(Guid personId, DateTime date = default(DateTime));
        Task<IEnumerable<Order>> FindByCustomer(Guid personId);
        Task<IEnumerable<Order>> FindByWorkshop(Guid workshopId);
        Task<BarStatisticOrderItem> BarOrdersByCustomer(Person customer, DateTime periodStart, DateTime periodEnd);
        Task<BarStatisticPaymentItem> BarPaymentByCustomer(Person customer, DateTime periodStart, DateTime periodEnd);
    }
}