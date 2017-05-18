using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace StudioBMS.Core.Entities.Base
{
    public class PersonClaim : IdentityUserClaim<Guid>
    {
    }
}