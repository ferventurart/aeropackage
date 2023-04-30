using System;
using FluentValidation;

namespace AeroPackage.Application.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
	public CreateCustomerCommandValidator()
	{
        RuleFor(x => x.FirstName)
               .NotEmpty()
               .MaximumLength(100);

        RuleFor(x => x.LastName)
               .NotEmpty()
               .MaximumLength(100);

        RuleFor(x => x.Email)
               .NotEmpty()
               .MaximumLength(250);

        RuleFor(x => x.Email)
               .EmailAddress();

        RuleFor(x => x.CellPhoneNumber)
               .NotEmpty()
               .Matches(@"^(?:-*\d-*){8}$");

        RuleFor(x => x.Gender)
              .NotEmpty()
              .InclusiveBetween(1, 2);

        RuleFor(x => x.Identification)
              .NotEmpty()
              .Matches(@"^\d{8}-\d{1}$");

        RuleFor(x => x.Address)
              .NotEmpty()
              .MaximumLength(200);

    }
}

