using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using StudioBMS.Business.DTO.Models;

namespace StudioBMS.Validators.Models
{
    public class PersonModelValidator: AbstractValidator<PersonModel>
    {
        public PersonModelValidator()
        {
            RuleFor(i => i.FirstName).NotEmpty();
            RuleFor(i => i.LastName).NotEmpty();
            RuleFor(i => i.Workshop.Id).NotEmpty();
            RuleFor(i => i.Birthday).Must(i => i != new DateTime());
        }
    }
}
