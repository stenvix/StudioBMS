using System.Threading.Tasks;
using StudioBMS.Business.Managers.Repositories.Interfaces;
using StudioBMS.Database.Context;
using StudioBMS.Repositories.Implementations;
using StudioBMS.Repositories.Interfaces;

namespace StudioBMS.Business.Managers.Repositories.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private PersonRepository _personRepository;
        private IWorkshopRepository _workshopRepository;
        private ITimeTableRepository _timeTableRepository;

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
        public PersonRepository PersonStore => _personRepository ?? (_personRepository = new PersonRepository(Context));
        public IWorkshopRepository WorkshopRepository => _workshopRepository ?? (_workshopRepository = new WorkshopRepository(Context));
        public ITimeTableRepository TimeTableRepository => _timeTableRepository ?? (_timeTableRepository = new TimeTableRepository(Context));
    }
}