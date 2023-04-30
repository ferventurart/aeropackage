using System;
using AeroPackage.Application.Customers.Commands.UpdateCustomer;
using FluentValidation;

namespace AeroPackage.Application.Customers.Commands.DeleteCustomer;

public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{
	public DeleteCustomerCommandValidator()
	{
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

