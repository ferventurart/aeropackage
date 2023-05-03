using System;
using FluentValidation;

namespace AeroPackage.Application.Packages.Queries.GetPackageByOwnTracker
{
	public class GetPackageByOwnTrackerQueryValidator : AbstractValidator<GetPackageByOwnTrackerQuery>
	{
		public GetPackageByOwnTrackerQueryValidator()
		{
			RuleFor(r => r.TrackingNumber).NotEmpty();
		}
	}
}

