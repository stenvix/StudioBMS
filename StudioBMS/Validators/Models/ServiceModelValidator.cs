using FluentValidation;
using StudioBMS.Business.DTO.Models;

namespace StudioBMS.Validators.Models
{
    public class ServiceModelValidator : AbstractValidator<ServiceModel>
    {
        public ServiceModelValidator()
        {
            RuleFor(i => i.EnTitle).NotEmpty();
            RuleFor(i => i.RuTitle).NotEmpty();
            RuleFor(i => i.UkTitle).NotEmpty();
            RuleFor(i => i.Price).GreaterThan(0.0);
        }
    }
}