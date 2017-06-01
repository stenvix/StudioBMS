using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using StudioBMS.Business.DTO.Models.Interfaces;
using StudioBMS.Business.Identity.Models;

namespace StudioBMS.Business.DTO.Models
{
    public class PersonModel : IdentityUser<Guid, PersonModelClaims, PersonModelRole, PersonModelLogin>, IModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public RoleModel Role { get; set; }
        public DateTime Birthday { get; set; }
        public WorkshopModel Workshop { get; set; }
        public IList<TimeTableModel> TimeTables { get; set; }
        public IList<OrderModel> Orders { get; set; }
    }
}