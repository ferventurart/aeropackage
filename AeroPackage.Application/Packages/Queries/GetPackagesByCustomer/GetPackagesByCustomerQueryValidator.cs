using System;
using FluentValidation;

namespace AeroPackage.Application.Packages.Queries.GetPackagesByCustomer;

public class GetPackagesByCustomerQueryValidator : AbstractValidator<GetPackageByCustomerQuery>
{
	public GetPackagesByCustomerQueryValidator()
	{
        RuleFor(r => r.Id)
            .NotEmpty();

        RuleFor(r => r.From)
            .NotEmpty()
            .LessThan(r => r.To);

        RuleFor(r => r.To)
            .NotEmpty()
            .GreaterThan(r => r.From);
    }
}

