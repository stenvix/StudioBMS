using StudioBMS.Business.Interfaces.Repositories.Base;
using StudioBMS.Core.Entities.IdentityBase;

namespace StudioBMS.Repositories.Interfaces
{
    public interface IRoleRepository : IRepository<Role>
    {
        Role Customer { get; }
        Role Administrator { get; }
        Role Manager { get; }
    }
}