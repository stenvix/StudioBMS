using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using StudioBMS.Core.Entities.Base;
using StudioBMS.Core.Entities.Interfaces;

namespace StudioBMS.Core.Entities.IdentityBase
{
    public class Role : IdentityRole<Guid, PersonRole, RoleClaim>, IEntity
    {
    }
}