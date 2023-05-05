using System;
using FluentValidation;

namespace AeroPackage.Application.Packages.Queries.GetPackagesExcelByPeriod
{
	public class GetPackagesExcelByPeriodQueryValidator : AbstractValidator<GetPackagesExcelByPeriodQuery>
	{
		public GetPackagesExcelByPeriodQueryValidator()
		{
            RuleFor(r => r.From)
                .NotEmpty()
                .LessThan(r => r.To);

            RuleFor(r => r.To)
                .NotEmpty()
                .GreaterThan(r => r.From);

            RuleFor(r => r.Status).NotEmpty();
        }
	}
}

