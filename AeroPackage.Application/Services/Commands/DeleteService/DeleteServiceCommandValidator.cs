using System;
using AeroPackage.Application.Services.Commands.UpdateService;
using FluentValidation;

namespace AeroPackage.Application.Services.Commands.DeleteService;

public class DeleteServiceCommandValidator : AbstractValidator<DeleteServiceCommand>
{
	public DeleteServiceCommandValidator()
	{
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

