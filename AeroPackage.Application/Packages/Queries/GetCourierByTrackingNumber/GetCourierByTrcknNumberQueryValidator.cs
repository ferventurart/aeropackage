using System;
using FluentValidation;

namespace AeroPackage.Application.Packages.Queries.GetCourierByTrackingNumber;

public class GetCourierByTrcknNumberQueryValidator : AbstractValidator<GetCourierByTrcknNumberQuery>
{
	public GetCourierByTrcknNumberQueryValidator()
	{
		RuleFor(r => r.TrackingNumber)
			.NotEmpty()
			.MaximumLength(80);
	}
}

