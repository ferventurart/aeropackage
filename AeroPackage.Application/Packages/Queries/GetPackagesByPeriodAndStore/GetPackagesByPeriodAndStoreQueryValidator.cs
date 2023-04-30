using System;
using FluentValidation;

namespace AeroPackage.Application.Packages.Queries.GetPackagesByPeriodAndStore;

public class GetPackagesByPeriodAndStoreQueryValidator : AbstractValidator<GetPackagesByPeriodAndStoreQuery>
{
	public GetPackagesByPeriodAndStoreQueryValidator()
	{
        RuleFor(r => r.from)
            .LessThan(r => r.to);

        RuleFor(r => r.to)
			.GreaterThan(r => r.from);
	}
}

