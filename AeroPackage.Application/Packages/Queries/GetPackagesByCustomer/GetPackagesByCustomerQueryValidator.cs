using System;
using FluentValidation;

namespace AeroPackage.Application.Packages.Queries.GetPackagesByCustomer;

public class GetPackagesByCustomerQueryValidator : AbstractValidator<GetPackageByCustomerQuery>
{
	public GetPackagesByCustomerQueryValidator()
	{
        RuleFor(r => r.Id)
            .NotEmpty();

        RuleFor(r => r.from)
            .LessThan(r => r.to);

        RuleFor(r => r.to)
            .GreaterThan(r => r.from);
    }
}

