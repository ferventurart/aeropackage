using System;
using FluentValidation;

namespace AeroPackage.Application.Customers.Queries.GetCustomers;

public class GetCustomersQueryValidator : AbstractValidator<GetCustomersQuery>
{
	public GetCustomersQueryValidator()
	{
		RuleFor(r => r.PageNumber)
			  .NotEmpty()
			  .InclusiveBetween(1, int.MaxValue);

        RuleFor(r => r.PageSize)
			  .NotEmpty()
			  .InclusiveBetween(10, 100);
    }
}

