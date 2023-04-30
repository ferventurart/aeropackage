using System;
using FluentValidation;

namespace AeroPackage.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
	public UpdateUserCommandValidator()
	{
        RuleFor(x => x.Id)
            .NotEmpty();
        RuleFor(x => x.FirstName)
            .NotEmpty();
        RuleFor(x => x.LastName)
            .NotEmpty();
        RuleFor(x => x.Email)
            .NotEmpty();
        RuleFor(x => x.Status)
            .InclusiveBetween(0, 1);
        RuleFor(x => x.Role)
            .InclusiveBetween(1, 3);
    }
}

