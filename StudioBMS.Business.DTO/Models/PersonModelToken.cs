using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace StudioBMS.Business.Identity.Models
{
    public class PersonModelToken : IdentityUserToken<Guid>
    {
    }
}