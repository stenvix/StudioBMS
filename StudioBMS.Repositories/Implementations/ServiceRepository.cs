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
    }
}