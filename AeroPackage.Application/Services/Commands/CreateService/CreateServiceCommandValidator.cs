using System;
using FluentValidation;

namespace AeroPackage.Application.Services.Commands.CreateService;

public class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
{
	public CreateServiceCommandValidator()
	{
        RuleFor(x => x.Name)
               .NotEmpty()
               .MaximumLength(60);

        RuleFor(x => x.Rate)
               .NotEmpty();

    }
}

