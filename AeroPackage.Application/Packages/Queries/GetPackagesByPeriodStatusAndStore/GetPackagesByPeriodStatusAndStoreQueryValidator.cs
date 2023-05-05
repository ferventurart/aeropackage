using System;
using FluentValidation;

namespace AeroPackage.Application.Packages.Queries.GetPackagesByPeriodStatusAndStore;

public class GetPackagesByPeriodStatusAndStoreQueryValidator : AbstractValidator<GetPackagesByPeriodStatusAndStoreQuery>
{
	public GetPackagesByPeriodStatusAndStoreQueryValidator()
	{
        RuleFor(r => r.From)
			.NotEmpty()
            .LessThan(r => r.To);

        RuleFor(r => r.To)
			.NotEmpty()
			.GreaterThan(r => r.From);
	}
}

