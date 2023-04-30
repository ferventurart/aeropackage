using System;
using FluentValidation;

namespace AeroPackage.Application.Customers.Queries.GetCustomerByIdentification
{
	public class GetCustomerByIdentificationQueryValidator : AbstractValidator<GetCustomerByIdentificationQuery>
	{
		public GetCustomerByIdentificationQueryValidator()
		{
            RuleFor(x => x.Identification)
                  .NotEmpty()
                  .Matches(@"^\d{8}-\d{1}$");
        }
	}
}

