﻿using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using StudioBMS.Core.Entities.Base;
using StudioBMS.Core.Entities.Interfaces;

namespace StudioBMS.Core.Entities
{
    public class Person : IdentityUser<Guid, PersonClaim, PersonRole, PersonLogin>, IEntity
    {
    }
}