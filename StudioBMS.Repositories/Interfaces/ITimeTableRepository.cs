using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using StudioBMS.Business.Interfaces.Repositories.Base;
using StudioBMS.Core.Entities;

namespace StudioBMS.Repositories.Interfaces
{
    public interface ITimeTableRepository : IRepository<Timetable>
    {
        Task<IQueryable<Timetable>> ByWorkshop(Guid workshopId, CancellationToken cancellationToken = default(CancellationToken));
    }
}