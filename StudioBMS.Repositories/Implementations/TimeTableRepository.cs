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
    public class TimeTableRepository : Repository<TimeTable>, ITimeTableRepository
    {
        public TimeTableRepository(StudioContext context) : base(context)
        {
        }

        public Task<IQueryable<TimeTable>> ByWorkshop(Guid workshopId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Run(() => Context.ItemTimeTables.Where(i => i.WorkshopId == workshopId).Select(i => i.TimeTable), cancellationToken);
        }
    }
}