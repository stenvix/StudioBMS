﻿using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using StudioBMS.Core.Entities.Base;
using StudioBMS.Core.Entities.IdentityBase;
using StudioBMS.Database.Context;

namespace StudioBMS.Repositories.Implementations
{
    public class RoleRepository : RoleStore<Role, StudioContext, Guid, PersonRole, RoleClaim>
    {
        public RoleRepository(StudioContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }

        protected override RoleClaim CreateRoleClaim(Role role, Claim claim)
        {
            return new RoleClaim {RoleId = role.Id, ClaimType = claim.Type, ClaimValue = claim.Value};
        }
    }
}