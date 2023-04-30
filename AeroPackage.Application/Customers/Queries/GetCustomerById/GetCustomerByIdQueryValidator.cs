using System;
using FluentValidation;

namespace AeroPackage.Application.Customers.Queries.GetCustomerById;

public class GetCustomerByIdQueryValidator : AbstractValidator<GetCustomerByIdQuery>
{
	public GetCustomerByIdQueryValidator()
	{
		RuleFor(r => r.Id)
			.NotEmpty();
	}
}

