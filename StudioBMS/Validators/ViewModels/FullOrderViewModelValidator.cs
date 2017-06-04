using FluentValidation;
using StudioBMS.Business.DTO.Models;

namespace StudioBMS.Validators.ViewModels
{
    public class FullOrderViewModelValidator : AbstractValidator<OrderViewModel>
    {
        public FullOrderViewModelValidator()
        {
            RuleFor(i => i.CustomerId).NotEmpty();
            RuleFor(i => i.PerformerId).NotEmpty();
            RuleFor(i => i.ServiceIds).NotEmpty();
            RuleFor(i => i.Date).NotEmpty();
        }
    }
}