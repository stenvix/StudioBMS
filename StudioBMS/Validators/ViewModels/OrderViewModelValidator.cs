using FluentValidation;
using Microsoft.AspNetCore.Mvc.Localization;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Messages;
using StudioBMS.Models;

namespace StudioBMS.Validators.ViewModels
{
    public class OrderViewModelValidator : AbstractValidator<OrderViewModel>
    {
        public OrderViewModelValidator(IHtmlLocalizer<ModelResource> modelLocalizer,
            IHtmlLocalizer<MessageResource> messageLocalizer)
        {
            RuleFor(i => i.ServiceIds)
                .NotEmpty().WithName(modelLocalizer["Services"].Value)
                .WithMessage(messageLocalizer["FieldRequire"].Value);

            RuleFor(i => i.FirstName)
                .NotEmpty().WithName(modelLocalizer["FirstName"].Value)
                .WithMessage(messageLocalizer["FieldRequire"].Value);

            RuleFor(i => i.Date)
                .NotNull().WithName(modelLocalizer["Date"].Value)
                .WithMessage(messageLocalizer["FieldRequire"].Value);

            RuleFor(i => i.LastName)
                .NotNull().WithName(modelLocalizer["LastName"].Value)
                .WithMessage(messageLocalizer["FieldRequire"].Value);

            RuleFor(i => i.EMail)
                .NotEmpty().WithName(modelLocalizer["EMail"].Value)
                .WithMessage(messageLocalizer["FieldRequire"].Value)
                .EmailAddress();

            RuleFor(i => i.Phone)
                .NotEmpty().WithName(modelLocalizer["Phone"].Value)
                .WithMessage(messageLocalizer["FieldRequire"].Value);
        }
    }
}