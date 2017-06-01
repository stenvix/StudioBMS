using StudioBMS.Business.Interfaces.Repositories.Base;
using StudioBMS.Core.Entities;

namespace StudioBMS.Repositories.Interfaces
{
    public interface IOrderStatusRepository : IRepository<OrderStatus>
    {
        OrderStatus Active { get; }
    }
}