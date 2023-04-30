using System;
using AeroPackage.Application.Customers.Commands.DeleteCustomer;
using FluentValidation;

namespace AeroPackage.Application.Users.Commands.DeleteUser;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

