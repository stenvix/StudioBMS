using System;
using System.Threading.Tasks;
using StudioBMS.Repositories.Implementations;
using StudioBMS.Repositories.Interfaces;

namespace StudioBMS.Business.Managers.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPersonRepository PersonRepository { get; }
        IRoleRepository RoleRepository { get; }
        PersonStoreRepository PersonStoreStore { get; }
        RoleStoreRepository RoleStoreRepository { get; }
        IWorkshopRepository WorkshopRepository { get;}
        ITimeTableRepository TimeTableRepository { get; }
        IServiceRepository ServiceRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderStatusRepository OrderStatusRepository { get; }
        IOrderServiceRepository OrderServiceRepository { get; }
        Task SaveChanges();
    }
}