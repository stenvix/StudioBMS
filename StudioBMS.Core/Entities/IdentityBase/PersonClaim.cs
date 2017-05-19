using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace StudioBMS.Core.Entities.IdentityBase
{
    public class PersonClaim : IdentityUserClaim<Guid>
    {
    }
}