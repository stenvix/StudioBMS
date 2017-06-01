using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudioBMS.Core.Entities;
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
    }
}