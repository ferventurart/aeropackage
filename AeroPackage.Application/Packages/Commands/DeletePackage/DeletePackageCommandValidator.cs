using System;
using FluentValidation;

namespace AeroPackage.Application.Packages.Commands.DeletePackage
{
	public class DeletePackageCommandValidator : AbstractValidator<DeletePackageCommand>
	{
		public DeletePackageCommandValidator()
		{
			RuleFor(r => r.Id).NotEmpty();
		}
	}
}

