using System;
using System.Threading.Tasks;
using StudioBMS.Repositories.Implementations;
using StudioBMS.Repositories.Interfaces;

namespace StudioBMS.Business.Managers.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        PersonRepository PersonStore { get; }
        Task SaveChanges();
    }
}