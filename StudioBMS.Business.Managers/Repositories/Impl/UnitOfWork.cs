using System.Threading.Tasks;
using StudioBMS.Business.Managers.Repositories.Interfaces;
using StudioBMS.Database.Context;
using StudioBMS.Repositories.Implementations;
using StudioBMS.Repositories.Interfaces;

namespace StudioBMS.Business.Managers.Repositories.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private RoleStoreRepository _roleStoreRepository;
        private PersonStoreRepository _personStoreRepository;
        private IWorkshopRepository _workshopRepository;
        private ITimeTableRepository _timeTableRepository;
        private IServiceRepository _serviceRepository;
        private IPersonRepository _personRepository;
        private IRoleRepository _roleRepository;

        public UnitOfWork(StudioContext context)
        {
            Context = context;
        }
        public Task SaveChanges()
        {
            return Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }

        private StudioContext Context { get; }
        public PersonStoreRepository PersonStoreStore => _personStoreRepository ?? (_personStoreRepository = new PersonStoreRepository(Context));
        public RoleStoreRepository RoleStoreRepository => _roleStoreRepository ?? (_roleStoreRepository = new RoleStoreRepository(Context));
        public IWorkshopRepository WorkshopRepository => _workshopRepository ?? (_workshopRepository = new WorkshopRepository(Context));
        public ITimeTableRepository TimeTableRepository => _timeTableRepository ?? (_timeTableRepository = new TimeTableRepository(Context));
        public IServiceRepository ServiceRepository => _serviceRepository ?? (_serviceRepository = new ServiceRepository(Context));
        public IPersonRepository PersonRepository => _personRepository ?? (_personRepository = new PersonRepository(Context));
        public IRoleRepository RoleRepository => _roleRepository ?? (_roleRepository = new RoleRepository(Context));
    }
}