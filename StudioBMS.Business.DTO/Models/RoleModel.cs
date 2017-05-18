using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace StudioBMS.Business.Identity.Models
{
    public class RoleModel : IdentityRole<Guid, PersonModelRole, RoleModelClaims>
    {
    }
}