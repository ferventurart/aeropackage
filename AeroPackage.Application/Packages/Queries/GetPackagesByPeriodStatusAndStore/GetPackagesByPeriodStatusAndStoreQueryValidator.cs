using System;
using FluentValidation;

namespace AeroPackage.Application.Packages.Queries.GetPackagesByPeriodStatusAndStore;

public class GetPackagesByPeriodStatusAndStoreQueryValidator : AbstractValidator<GetPackagesByPeriodStatusAndStoreQuery>
{
	public GetPackagesByPeriodStatusAndStoreQueryValidator()
	{
        RuleFor(r => r.from)
            .LessThan(r => r.to);

        RuleFor(r => r.to)
			.GreaterThan(r => r.from);
	}
}

