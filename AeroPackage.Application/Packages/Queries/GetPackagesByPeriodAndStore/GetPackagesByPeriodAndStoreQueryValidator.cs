using System;
using FluentValidation;

namespace AeroPackage.Application.Packages.Queries.GetPackagesByPeriodAndStore;

public class GetPackagesByPeriodAndStoreQueryValidator : AbstractValidator<GetPackagesByPeriodAndStoreQuery>
{
	public GetPackagesByPeriodAndStoreQueryValidator()
	{
        RuleFor(r => r.From)
            .NotEmpty()
            .LessThan(r => r.To);

        RuleFor(r => r.To)
            .NotEmpty()
            .GreaterThan(r => r.From);
    }
}

