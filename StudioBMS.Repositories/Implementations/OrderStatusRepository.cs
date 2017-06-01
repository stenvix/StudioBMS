using System;
using System.Linq;
using StudioBMS.Core.Entities;
using StudioBMS.Database.Context;
using StudioBMS.Repositories.Implementations.Base;
using StudioBMS.Repositories.Interfaces;

namespace StudioBMS.Repositories.Implementations
{
    public class OrderStatusRepository : Repository<OrderStatus>, IOrderStatusRepository
    {
        public OrderStatusRepository(StudioContext context) : base(context)
        {
        }

        public OrderStatus Active
        {
            get { return Set.FirstOrDefault(i => i.Name == StringConstants.ActiveStatus); }
        }

        public OrderStatus Declined
        {
            get { return Set.FirstOrDefault(i => i.Name == StringConstants.DeclinedStatus); }
        }

        public OrderStatus Done
        {
            get { return Set.FirstOrDefault(i => i.Name == StringConstants.DoneStatus); }
        }
    }
}