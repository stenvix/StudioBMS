using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using StudioBMS.Business.DTO.Models.Interfaces;
using StudioBMS.Business.Identity.Models;

namespace StudioBMS.Business.DTO.Models
{
    public class PersonModel : IdentityUser<Guid, PersonModelClaims, PersonModelRole, PersonModelLogin>, IModel
    {
        [Display(Name = nameof(FirstName), ResourceType = typeof(Properties.DataAnnotations))]
        public string FirstName { get; set; }
        [Display(Name = nameof(LastName), ResourceType = typeof(Properties.DataAnnotations))]
        public string LastName { get; set; }
        
        [Display(Name = nameof(Birthday), ResourceType = typeof(Properties.DataAnnotations))]
        public DateTime Birthday { get; set; }

        [Display(Name = nameof(PhoneNumber), ResourceType = typeof(Properties.DataAnnotations))]
        public override string PhoneNumber { get; set; }
    }
}