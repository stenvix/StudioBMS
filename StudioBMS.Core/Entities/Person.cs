using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using StudioBMS.Core.Entities.IdentityBase;
using StudioBMS.Core.Entities.Interfaces;

namespace StudioBMS.Core.Entities
{
    public class Person : IdentityUser<Guid, PersonClaim, PersonRole, PersonLogin>, IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public Guid WorkshopId { get; set; }
        public Workshop Workshop { get; set; }
        public IEnumerable<PersonTimetable> PersonTimetables { get; set; }
        public IEnumerable<PersonService> PersonServices { get; set; }
    }
}