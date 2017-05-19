using System;
using System.Collections.Generic;
using System.Text;
using StudioBMS.Core.Entities;
using StudioBMS.Core.Entities.IdentityBase;

namespace StudioBMS.Database.Context
{
    public static class DatabaseInitializer
    {
        public static void Initialize(this StudioContext context)
        {
            var roles = new[]
            {
                "client",
                "manager",
                "employee",
                "administrator"
            };
            foreach (var roleName in roles)
            {
                var role = new Role { Name = roleName.ToLower(), NormalizedName = roleName.ToUpper() };
                context.Roles.Add(role);
                context.SaveChanges();
            }
        }
    }
}
