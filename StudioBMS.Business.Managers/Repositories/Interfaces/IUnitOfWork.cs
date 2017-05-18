using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StudioBMS.Core.Entities;
using StudioBMS.Repositories.Interfaces;

namespace StudioBMS.Business.Managers.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserStore<Person> PersonStore { get; }
        IPersonRepository PersonRepository { get; }
        Task SaveChanges();
    }
}