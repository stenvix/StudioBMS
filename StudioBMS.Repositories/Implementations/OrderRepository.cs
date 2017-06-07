using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudioBMS.Core.Entities;
using StudioBMS.Core.Entities.Statistics;
using StudioBMS.Database.Context;
using StudioBMS.Repositories.Implementations.Base;
using StudioBMS.Repositories.Interfaces;

namespace StudioBMS.Repositories.Implementations
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(StudioContext context) : base(context)
        {
        }

        protected override IQueryable<Order> Include()
        {
            return Set
                .Include(i => i.Customer)
                .Include(i => i.Performer)
                .Include(i => i.Workshop)
                .Include(i => i.Status)
                .Include(i => i.OrderServices)
                    .ThenInclude(i => i.Service);
        }

        public Task<IEnumerable<Order>> FindByPerformer(Guid performerId, DateTime date = default(DateTime))
        {
            return Task.Run(() => Include().Where(i => i.PerformerId == performerId && i.Date.Date == date.Date).AsEnumerable());
        }

        public Task<IEnumerable<Order>> FindByCustomer(Guid personId)
        {
            return Task.Run(() => Include().Where(i => i.CustomerId == personId).AsEnumerable());
        }

        public Task<IEnumerable<Order>> FindByWorkshop(Guid workshopId)
        {
            return Task.Run(() => Include().Where(i => i.WorkshopId == workshopId).AsEnumerable());
        }

        private IQueryable<Order> FindByCustomerInPeriod(Guid customerId, DateTime periodStart,
            DateTime periodEnd)
        {
            return Include().Where(i => i.CustomerId == customerId && i.Date >= periodStart && i.Date <= periodEnd);
        }
        
        public Task<BarStatisticOrderItem> BarOrdersByCustomer(Person customer, DateTime periodStart, DateTime periodEnd)
        {
            var barItem = new BarStatisticOrderItem();

            var orders = FindByCustomerInPeriod(customer.Id, periodStart, periodEnd);
            barItem.Label = $"{customer.LastName} {customer.FirstName[0]}.";
            barItem.Active = orders.Count(i => i.Status.Name == StringConstants.ActiveStatus);
            barItem.Done = orders.Count(i => i.Status.Name == StringConstants.DoneStatus);
            barItem.Declined = orders.Count(i => i.Status.Name == StringConstants.DeclinedStatus);

            return Task.FromResult(barItem);
        }

        public Task<BarStatisticPaymentItem> BarPaymentByCustomer(Person customer, DateTime periodStart, DateTime periodEnd)
        {
            var barItem = new BarStatisticPaymentItem();
            var orders = FindByCustomerInPeriod(customer.Id, periodStart, periodEnd);

            barItem.PriceAmount = orders.Select(i => i.Price).Sum();
            barItem.BalanceAmount = orders.Select(i => i.Balance).Sum();

            return Task.FromResult(barItem);
        }
    }
}