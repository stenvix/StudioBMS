using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using StudioBMS.Business.DTO.Models.Interfaces;
using StudioBMS.Business.Identity.Models;

namespace StudioBMS.Business.DTO.Models
{
    public class RoleModel : IdentityRole<Guid, PersonModelRole, RoleModelClaims>, IModel
    {
    }
}