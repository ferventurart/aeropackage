using System;
using FluentValidation;

namespace AeroPackage.Application.Users.Commands.CreateUser
{
	public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
	{
		public CreateUserCommandValidator()
		{
            RuleFor(x => x.FirstName)
                .NotEmpty();
            RuleFor(x => x.LastName)
                .NotEmpty();
            RuleFor(x => x.Email)
                .NotEmpty();
            RuleFor(x => x.Password)
                .NotEmpty();
            RuleFor(x => x.Role)
                .InclusiveBetween(1, 3);
        }
	}
}

