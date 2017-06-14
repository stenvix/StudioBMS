using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using StudioBMS.Business.DTO.Models.Interfaces;
using StudioBMS.Business.Identity.Models;

namespace StudioBMS.Business.DTO.Models
{
    public class PersonModel : IdentityUser<Guid, PersonModelClaims, PersonModelRole, PersonModelLogin>, IModel
    {
        public bool IsWorker => Role.Name != StringConstants.CustomerRole && Role.Name != StringConstants.AdministratorRole && Role.Name != StringConstants.ManagerRole;

        public string FullName => $"{LastName} {FirstName}";
        public string FullNameAbbr => $"{LastName} {FirstName[0]}.";
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Language { get; set; }
        public RoleModel Role { get; set; }
        public DateTime Birthday { get; set; }
        public WorkshopModel Workshop { get; set; }
        public IList<ServiceModel> Services { get; set; }
        public IList<TimeTableModel> TimeTables { get; set; }
        public IList<OrderModel> Orders { get; set; }
    }
}