using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StudioBMS.Business.Managers.Repositories.Interfaces;
using StudioBMS.Core.Entities;
using StudioBMS.Database.Context;
using StudioBMS.Repositories.Implementations;
using StudioBMS.Repositories.Interfaces;

namespace StudioBMS.Business.Managers.Repositories.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private IPersonRepository _personRepository;
        private IUserStore<Person> _personStore;

        public UnitOfWork(StudioContext context)
        {
            Context = context;
        }

        private StudioContext Context { get; }

        public IUserStore<Person> PersonStore => _personStore ??
                                                 (IUserStore<Person>) (_personRepository =
                                                     new PersonRepository(Context));

        public IPersonRepository PersonRepository => _personRepository ??
                                                     (_personRepository = new PersonRepository(Context));

        public Task SaveChanges()
        {
            return Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}