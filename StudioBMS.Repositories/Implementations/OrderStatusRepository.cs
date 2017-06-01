using System.Linq;
using StudioBMS.Core.Entities;
using StudioBMS.Database.Context;
using StudioBMS.Repositories.Implementations.Base;
using StudioBMS.Repositories.Interfaces;

namespace StudioBMS.Repositories.Implementations
{
    public class OrderStatusRepository : Repository<OrderStatus>, IOrderStatusRepository
    {
        private static readonly string ACTIVE = "Active";
        public OrderStatusRepository(StudioContext context) : base(context)
        {
        }

        public OrderStatus Active
        {
            get { return Set.FirstOrDefault(i => i.Name == ACTIVE); }
        }
    }
}