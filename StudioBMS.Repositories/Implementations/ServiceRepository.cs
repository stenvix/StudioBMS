using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        protected override IQueryable<Service> Include()
        {
            return Set.Where(i => i.IsActive);
        }

        public Task<IEnumerable<Service>> FindByPerson(Guid personId)
        {
            var ids = Context.PersonServices.Where(i => i.PersonId == personId).Select(s => s.ServiceId);
            return Task.Run(() => Include().Where(i => ids.Contains(i.Id)).AsEnumerable());
        }

        public Task CreatePerson(Guid personId, Guid serviceId)
        {
            return Context.PersonServices.AddAsync(new PersonService { PersonId = personId, ServiceId = serviceId });
        }

        public Task DeletePersonService(Guid personId, Guid serviceId)
        {
            return Task.Run(() =>
            {
                var entity = Context.PersonServices.FirstOrDefault(i => i.PersonId == personId && i.ServiceId == serviceId);
                Context.PersonServices.Remove(entity);
            });
        }

        public override async Task DeleteAsync(Service entity, CancellationToken cancellationToken = new CancellationToken())
        {
            entity.IsActive = false;
            foreach (var pts in Context.PersonServices.Where(i => i.ServiceId == entity.Id))
            {
                Context.PersonServices.Remove(pts);
            }
            await Update(entity, cancellationToken);
        }
    }
}