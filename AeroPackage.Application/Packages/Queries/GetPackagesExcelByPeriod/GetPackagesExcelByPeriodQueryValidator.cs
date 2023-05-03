using System;
using FluentValidation;

namespace AeroPackage.Application.Packages.Queries.GetPackagesExcelByPeriod
{
	public class GetPackagesExcelByPeriodQueryValidator : AbstractValidator<GetPackagesExcelByPeriodQuery>
	{
		public GetPackagesExcelByPeriodQueryValidator()
		{
			RuleFor(r => r.From).NotEmpty();

            RuleFor(r => r.To).NotEmpty();

            RuleFor(r => r.Status).NotEmpty();
        }
	}
}

