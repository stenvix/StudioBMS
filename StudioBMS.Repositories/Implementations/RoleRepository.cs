using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public Task<Role> FindByName(string name)
        {
            return Task.FromResult(Include().FirstOrDefault(i => i.Name.ToUpper().Normalize() == name.ToUpper().Normalize()));
        }

        public Task<IEnumerable<Role>> GetWorkerRoles()
        {
            var ids = new List<Guid> { Administrator.Id, Manager.Id, Customer.Id };
            return Task.Run(() => Include().Where(i => !ids.Contains(i.Id)).AsEnumerable());
        }

        public Task ClearRoles(Guid personId)
        {
            return Task.Run(() =>
            {
                foreach (var personRole in Context.UserRoles.Where(i => i.UserId == personId))
                {
                    Context.UserRoles.Remove(personRole);
                }
            });
        }

        public Task AddToRole(Guid personId, Guid roleId)
        {
            return Context.UserRoles.AddAsync(new PersonRole { UserId = personId, RoleId = roleId });
        }

        public Task<bool> IsInRole(Guid personId, Guid roleId)
        {
            return Task.FromResult(Context.UserRoles.Any(i => i.UserId == personId && i.RoleId == roleId));
        }
    }
}