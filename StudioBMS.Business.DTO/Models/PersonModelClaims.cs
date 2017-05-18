using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace StudioBMS.Business.Identity.Models
{
    public class PersonModelClaims : IdentityUserClaim<Guid>
    {
    }
}