using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudioBMS.Business.Interfaces.Repositories.Base;
using StudioBMS.Core.Entities;
using StudioBMS.Core.Entities.Statistics;

namespace StudioBMS.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IQueryable<Order>> FindByPerformer(Guid personId, DateTime date = default(DateTime));
        Task<IQueryable<Order>> FindByPerformer(Guid personId, IQueryable<Order> orders = default(IQueryable<Order>));
        Task<IQueryable<Order>> FindByCustomer(Guid personId, IQueryable<Order> orders = default(IQueryable<Order>));
        Task<IQueryable<Order>> FindByWorkshop(Guid workshopId, IQueryable<Order> orders = default(IQueryable<Order>));
        Task<IQueryable<Order>> FindInPeriod(DateTime periodStart, DateTime periodEnd, IQueryable<Order> orders = default(IQueryable<Order>));

        Task<BarStatisticOrderItem> BarOrdersByPerson(Person customer, IQueryable<Order> orders);
        Task<BarStatisticPaymentItem> BarPaymentByPerson(Person customer, IQueryable<Order> orders);
        Task<BarStatisticOrderItem> BarOrdersByWorkshop(Workshop workshop, IQueryable<Order> orders);
        Task<BarStatisticPaymentItem> BarPaymentByWorkshop(Workshop workshop, IQueryable<Order> orders);

        Task<PieStatistic> PieStatisticByWorkers(Guid[] workers, IQueryable<Order> orders);
        Task<PieStatistic> PieStatisticByWorkshop(Guid[] workshops, IQueryable<Order> orders);
        Task<PieStatistic> PieStatisticByCustomers(Guid[] customers, IQueryable<Order> orders);

        Task<List<AvarageBillStatistic>> AvarageBillsByCustomers(Guid[] customers, IQueryable<Order> orders, DateTime periodStart, DateTime periodEnd);

        Task<List<AvarageBillStatistic>> AvarageBillsByWorkers(Guid[] workers, IQueryable<Order> orders, DateTime periodStart, DateTime periodEnd);
        Task<List<AvarageBillStatistic>> AvarageBillsByWorkshops(Guid[] workshops, IQueryable<Order> orders, DateTime periodStart, DateTime periodEnd);



    }
}