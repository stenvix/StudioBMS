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

        public Task<IQueryable<Order>> FindByPerformer(Guid performerId, DateTime date = default(DateTime))
        {
            return Task.FromResult(Include().Where(i => i.PerformerId == performerId && i.Date.Date == date.Date));
        }

        public Task<IQueryable<Order>> FindByPerformer(Guid personId, IQueryable<Order> orders = default(IQueryable<Order>))
        {
            if (orders != null)
            {
                return Task.FromResult(orders.Where(i => i.PerformerId == personId));
            }

            return Task.FromResult(Include().Where(i => i.PerformerId == personId));
        }

        public Task<IQueryable<Order>> FindByCustomer(Guid personId, IQueryable<Order> orders = default(IQueryable<Order>))
        {
            if (orders != null)
            {
                return Task.FromResult(orders.Where(i => i.CustomerId == personId));
            }

            return Task.FromResult(Include().Where(i => i.CustomerId == personId));
        }

        public Task<IQueryable<Order>> FindByWorkshop(Guid workshopId, IQueryable<Order> orders = default(IQueryable<Order>))
        {
            if (orders != null)
            {
                return Task.FromResult(orders.Where(i => i.WorkshopId == workshopId));
            }

            return Task.FromResult(Include().Where(i => i.WorkshopId == workshopId));
        }

        public Task<IQueryable<Order>> FindInPeriod(DateTime periodStart, DateTime periodEnd, IQueryable<Order> orders = default(IQueryable<Order>))
        {
            if (orders != null)
            {
                return Task.FromResult(orders.Where(i => i.Date >= periodStart.Date && i.Date <= periodEnd.Date.AddDays(1)));
            }

            return Task.FromResult(Include().Where(i => i.Date >= periodStart.Date && i.Date <= periodEnd.Date.AddDays(1)));
        }

        private IQueryable<Order> FindWithStatus(string status, IQueryable<Order> set = default(IQueryable<Order>))
        {
            return set.Where(i => i.Status.Name == status);
        }


        public Task<BarStatisticOrderItem> BarOrdersByPerson(Person person, IQueryable<Order> orders)
        {
            var barItem = new BarStatisticOrderItem
            {
                Label = $"{person.LastName} {person.FirstName[0]}.",
                Active = orders.Count(i => i.Status.Name == StringConstants.ActiveStatus),
                Done = orders.Count(i => i.Status.Name == StringConstants.DoneStatus),
                Declined = orders.Count(i => i.Status.Name == StringConstants.DeclinedStatus)
            };
            return Task.FromResult(barItem);
        }

        public Task<BarStatisticPaymentItem> BarPaymentByPerson(Person person, IQueryable<Order> orders)
        {
            var barItem = new BarStatisticPaymentItem
            {
                Label = $"{person.LastName} {person.FirstName[0]}",
                PriceAmount = orders.Select(i => i.Price).Sum() / 100.0,
                BalanceAmount = orders.Select(i => i.Balance).Sum() / 100.0
            };
            return Task.FromResult(barItem);
        }

        public Task<BarStatisticOrderItem> BarOrdersByWorkshop(Workshop workshop, IQueryable<Order> orders)
        {
            var barItem = new BarStatisticOrderItem
            {
                Label = $"{workshop.Title} ({workshop.City})",
                Active = orders.Count(i => i.Status.Name == StringConstants.ActiveStatus),
                Done = orders.Count(i => i.Status.Name == StringConstants.DoneStatus),
                Declined = orders.Count(i => i.Status.Name == StringConstants.DeclinedStatus)
            };
            return Task.FromResult(barItem);
        }

        public Task<BarStatisticPaymentItem> BarPaymentByWorkshop(Workshop workshop, IQueryable<Order> orders)
        {
            var barItem = new BarStatisticPaymentItem
            {
                Label = $"{workshop.Title} ({workshop.City})",
                PriceAmount = orders.Select(i => i.Price).Sum() / 100.0,
                BalanceAmount = orders.Select(i => i.Balance).Sum() / 100.0
            };
            return Task.FromResult(barItem);
        }

        private Task<AvarageBillStatistic> AvarageOrdersBill(IQueryable<Order> orders, DateTime date)
        {
            var bill = new AvarageBillStatistic { Date = date };
            if (orders.Any())
            {
                var balance = orders.Average(o => o.Balance);
                var price = orders.Average(o => o.Price);
                bill.Balance = balance / 100.0;
                bill.Price = price / 100.0;
            }

            return Task.FromResult(bill);
        }

        public Task<PieStatistic> PieStatisticByCustomers(Guid[] customers, IQueryable<Order> orders)
        {
            orders = orders.Where(i => customers.Contains(i.CustomerId));
            var pie = new PieStatistic
            {
                Active = FindWithStatus(StringConstants.ActiveStatus, orders)
                    .Count(),
                Declined = FindWithStatus(StringConstants.DeclinedStatus, orders)
                    .Count(),
                Done = FindWithStatus(StringConstants.DoneStatus, orders)
                    .Count()
            };
            return Task.FromResult(pie);
        }

        public Task<PieStatistic> PieStatisticByWorkers(Guid[] workers, IQueryable<Order> orders)
        {
            orders = orders.Where(i => workers.Contains(i.PerformerId));
            var pie = new PieStatistic
            {
                Active = FindWithStatus(StringConstants.ActiveStatus, orders)
                    .Count(),
                Declined = FindWithStatus(StringConstants.DeclinedStatus, orders)
                    .Count(),
                Done = FindWithStatus(StringConstants.DoneStatus, orders)
                    .Count()
            };
            return Task.FromResult(pie);
        }

        public Task<PieStatistic> PieStatisticByWorkshop(Guid[] workshops, IQueryable<Order> orders)
        {
            orders = orders.Where(i => workshops.Contains(i.WorkshopId));
            var pie = new PieStatistic
            {
                Active = FindWithStatus(StringConstants.ActiveStatus, orders)
                    .Count(i => workshops.Any(c => c == i.WorkshopId)),
                Declined = FindWithStatus(StringConstants.DeclinedStatus, orders)
                    .Count(i => workshops.Any(c => c == i.WorkshopId)),
                Done = FindWithStatus(StringConstants.DoneStatus, orders)
                    .Count(i => workshops.Any(c => c == i.WorkshopId))
            };
            return Task.FromResult(pie);
        }

        public async Task<List<AvarageBillStatistic>> AvarageBillsByCustomers(Guid[] customers, IQueryable<Order> orders, DateTime periodStart, DateTime periodEnd)
        {
            var bills = new List<AvarageBillStatistic>();
            var days = (periodEnd.AddDays(1) - periodStart).Days;
            for (int i = 0; i < days; i++)
            {
                var date = periodStart.AddDays(i);
                bills.Add(await AvarageOrdersBill((await FindInPeriod(date, date, orders)).Where(c => customers.Contains(c.CustomerId)), date));
            }
            return bills;
        }

        public async Task<List<AvarageBillStatistic>> AvarageBillsByWorkers(Guid[] workers, IQueryable<Order> orders, DateTime periodStart, DateTime periodEnd)
        {
            var bills = new List<AvarageBillStatistic>();
            var days = (periodEnd.AddDays(1) - periodStart).Days;
            for (int i = 0; i < days; i++)
            {
                var date = periodStart.AddDays(i);
                bills.Add(await AvarageOrdersBill((await FindInPeriod(date, date, orders)).Where(c => workers.Contains(c.PerformerId)), date));
            }
            return bills;
        }

        public async Task<List<AvarageBillStatistic>> AvarageBillsByWorkshops(Guid[] workshops, IQueryable<Order> orders, DateTime periodStart, DateTime periodEnd)
        {
            var bills = new List<AvarageBillStatistic>();
            var days = (periodEnd.AddDays(1) - periodStart).Days;
            for (int i = 0; i < days; i++)
            {
                var date = periodStart.AddDays(i);
                bills.Add(await AvarageOrdersBill((await FindInPeriod(date, date, orders)).Where(c => workshops.Contains(c.WorkshopId)), date));
            }
            return bills;
        }
    }
}