using System;
using FluentValidation;

namespace AeroPackage.Application.Services.Queries.GetServiceById;

public class GetServiceByIdQueryValidator : AbstractValidator<GetServiceByIdQuery>
{
	public GetServiceByIdQueryValidator()
	{
		RuleFor(r => r.Id)
			.NotEmpty();
	}
}

