using System;
using FluentValidation;

namespace AeroPackage.Application.Packages.Commands.CreatePackage;

public class CreatePackageCommandValidator : AbstractValidator<CreatePackageCommand>
{
	public CreatePackageCommandValidator()
	{
        RuleFor(x => x.Store)
           .NotEmpty()
           .MaximumLength(100);

        RuleFor(x => x.Courier)
           .MaximumLength(100);

        RuleFor(x => x.CourierTrackingNumber)
           .MaximumLength(100);

        RuleFor(x => x.Weight)
            .NotEmpty()
            .GreaterThan(0.00M);

        RuleFor(x => x.QuantityArticles)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.Description)
           .MaximumLength(100)
           .NotEmpty();

        RuleFor(x => x.DeclaredValue)
            .NotEmpty()
            .GreaterThan(0.00M);
    }
}

