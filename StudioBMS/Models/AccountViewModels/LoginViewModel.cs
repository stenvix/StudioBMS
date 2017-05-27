using System.ComponentModel.DataAnnotations;
using StudioBMS.Business.DTO.Properties;

namespace StudioBMS.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = nameof(RememberMe), ResourceType = typeof(DataAnnotations))]
        public bool RememberMe { get; set; }
    }
}