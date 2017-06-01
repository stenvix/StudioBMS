using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudioBMS.Core.Entities;
using StudioBMS.Database.Context;
using StudioBMS.Repositories.Implementations.Base;
using StudioBMS.Repositories.Interfaces;

namespace StudioBMS.Repositories.Implementations
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        public ServiceRepository(StudioContext context) : base(context)
        {
        }

        public Task<IEnumerable<Service>> FindByPerson(Guid personId)
        {
            var ids = Context.PersonServices.Where(i => i.PersonId == personId).Select(s => s.ServiceId);
            return Task.Run(() => Include().Where(i => ids.Contains(i.Id)).AsEnumerable());
        }
    }
}