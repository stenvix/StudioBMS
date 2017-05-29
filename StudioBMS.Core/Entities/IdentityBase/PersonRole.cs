using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace StudioBMS.Core.Entities.IdentityBase
{
    public class PersonRole : IdentityUserRole<Guid>
    {
        public Role Role { get; set; }
        public Person Person { get; set; }
    }
}