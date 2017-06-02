using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using StudioBMS.Core.Entities;
using StudioBMS.Database.Context;
using StudioBMS.Repositories.Implementations.Base;
using StudioBMS.Repositories.Interfaces;

namespace StudioBMS.Repositories.Implementations
{
    public class TimeTableRepository : Repository<Timetable>, ITimeTableRepository
    {
        public TimeTableRepository(StudioContext context) : base(context)
        {
        }

        public Task<IQueryable<Timetable>> ByWorkshop(Guid workshopId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Run(() => Context.WorkshopTimetables.Where(i => i.WorkshopId == workshopId).Select(i => i.Timetable), cancellationToken);
        }

        public Task<IQueryable<Timetable>> FindByWorker(Guid workerId, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.Run(() => Context.PersonTimetables.Where(i => i.PersonId == workerId).Select(i=>i.Timetable), cancellationToken);
        }

        public Task CreateWorkshop(Guid workshopId, Guid timetableId)
        {
            return Context.WorkshopTimetables.AddAsync(new WorkshopTimetable{WorkshopId = workshopId, TimetableId = timetableId});
        }

        public Task CreateWorker(Guid workerId, Guid timetableId)
        {
            return Context.PersonTimetables.AddAsync(new PersonTimetable {PersonId = workerId, TimetableId = timetableId});
        }
    }
}