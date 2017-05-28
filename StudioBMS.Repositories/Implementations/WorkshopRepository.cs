using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudioBMS.Core.Entities;
using StudioBMS.Database.Context;
using StudioBMS.Repositories.Implementations.Base;
using StudioBMS.Repositories.Interfaces;

namespace StudioBMS.Repositories.Implementations
{
    public class WorkshopRepository : Repository<Workshop>, IWorkshopRepository
    {
        public WorkshopRepository(StudioContext context) : base(context)
        {
        }

        public override Task<IQueryable<Workshop>> GetAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.Run(() => Set.Include(i => i.ItemTimeTables).ThenInclude(i => i.TimeTable).AsQueryable(), cancellationToken);
        }
    }
}