﻿using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using StudioBMS.Business.Identity.Models;

namespace StudioBMS.Business.DTO.Models
{
    public class PersonModel : IdentityUser<Guid, PersonModelClaims, PersonModelRole, PersonModelLogin>
    {
    }
}