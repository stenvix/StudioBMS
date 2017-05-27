using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using StudioBMS.Business.DTO.Models.Interfaces;
using StudioBMS.Business.Identity.Models;

namespace StudioBMS.Business.DTO.Models
{
    public class PersonModel : IdentityUser<Guid, PersonModelClaims, PersonModelRole, PersonModelLogin>, IModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public override string PhoneNumber { get; set; }
    }
}