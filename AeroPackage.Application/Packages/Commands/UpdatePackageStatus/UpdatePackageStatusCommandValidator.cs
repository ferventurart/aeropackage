using System;
using FluentValidation;

namespace AeroPackage.Application.Packages.Commands.UpdatePackageStatus;

public class UpdatePackageStatusCommandValidator : AbstractValidator<UpdatePackageStatusCommand>
{
	public UpdatePackageStatusCommandValidator()
	{
		RuleFor(r => r.OwnTrackingNumber).NotEmpty();

        RuleFor(r => r.Status).NotEmpty();
    }
}

