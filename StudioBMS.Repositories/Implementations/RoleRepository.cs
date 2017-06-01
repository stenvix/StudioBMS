using System.Linq;
using StudioBMS.Core.Entities.IdentityBase;
using StudioBMS.Database.Context;
using StudioBMS.Repositories.Implementations.Base;
using StudioBMS.Repositories.Interfaces;

namespace StudioBMS.Repositories.Implementations
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(StudioContext context) : base(context)
        {
        }

        public Role Customer
        {
            get { return Include().FirstOrDefault(i => i.Name.ToUpper() == StringConstants.CustomerRole); }
        }

        public Role Administrator
        {
            get { return Include().FirstOrDefault(i => i.Name.ToUpper() == StringConstants.AdministratorRole); }
        }

        public Role Manager
        {
            get { return Include().FirstOrDefault(i => i.Name.ToUpper() == StringConstants.ManagerRole); }
        }
    }
}