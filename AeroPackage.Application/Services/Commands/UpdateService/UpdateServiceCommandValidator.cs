using System;
using AeroPackage.Application.Services.Commands.CreateService;
using AeroPackage.Domain.ServicesAggregate.Enums;
using FluentValidation;

namespace AeroPackage.Application.Services.Commands.UpdateService;

public class UpdateServiceCommandValidator : AbstractValidator<UpdateServiceCommand>
{
    public UpdateServiceCommandValidator()
    {
        RuleFor(x => x.Id)
               .NotEmpty();

        RuleFor(x => x.Name)
               .NotEmpty()
               .MaximumLength(60);

        RuleFor(x => x.Rate)
               .NotEmpty();
    }
}

