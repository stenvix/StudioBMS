using System.Linq;
using StudioBMS.Core.Entities.IdentityBase;
using StudioBMS.Database.Context;
using StudioBMS.Repositories.Implementations.Base;
using StudioBMS.Repositories.Interfaces;

namespace StudioBMS.Repositories.Implementations
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private static string CLIENT = "CLIENT";
        private static string ADMINISTRATOR = "ADMINISTRATOR";
        private static string MANAGER = "MANAGER";
        public RoleRepository(StudioContext context) : base(context)
        {
        }

        public Role Client
        {
            get { return Include().FirstOrDefault(i => i.Name.ToUpper() == CLIENT); }
        }

        public Role Administrator
        {
            get { return Include().FirstOrDefault(i => i.Name.ToUpper() == ADMINISTRATOR); }
        }

        public Role Manager
        {
            get { return Include().FirstOrDefault(i => i.Name.ToUpper() == MANAGER); }
        }
    }
}