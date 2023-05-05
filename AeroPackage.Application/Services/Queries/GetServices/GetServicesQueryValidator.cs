using System;
using FluentValidation;

namespace AeroPackage.Application.Services.Queries.GetServices;

public class GetServicesQueryValidator : AbstractValidator<GetServicesQuery>
{
	public GetServicesQueryValidator()
	{
		RuleFor(r => r.PageNumber)
			  .NotEmpty()
			  .InclusiveBetween(1, int.MaxValue);

        RuleFor(r => r.PageSize)
			  .NotEmpty()
			  .InclusiveBetween(10, 100);
    }
}

