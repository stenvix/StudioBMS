using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using StudioBMS.Business.Interfaces.Repositories.Base;
using StudioBMS.Core.Entities;

namespace StudioBMS.Repositories.Interfaces
{
    public interface ITimeTableRepository : IRepository<TimeTable>
    {
        Task<IQueryable<TimeTable>> ByWorkshop(Guid workshopId, CancellationToken cancellationToken = default(CancellationToken));
    }
}