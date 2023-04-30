using System;
using AeroPackage.Application.Customers.Queries.GetCustomersActiveByName;
using FluentValidation;
namespace AeroPackage.Application.Customers.Queries.GetCustomersActiveByName;

public class GetCustomersActiveByNameQueryValidator : AbstractValidator<GetCustomersActiveByNameQuery>
{
	public GetCustomersActiveByNameQueryValidator()
	{
        RuleFor(r => r.Name)
          .NotEmpty()
          .MaximumLength(200);
        }
}

