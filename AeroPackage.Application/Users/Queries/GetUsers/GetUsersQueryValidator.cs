using System;
using FluentValidation;

namespace AeroPackage.Application.Users.Queries.GetUsers
{
	public class GetUsersQueryValidator : AbstractValidator<GetUsersQuery>
	{
		public GetUsersQueryValidator()
		{
            RuleFor(r => r.PageNumber)
              .NotEmpty()
              .InclusiveBetween(1, int.MaxValue);

            RuleFor(r => r.PageSize)
                  .NotEmpty()
                  .InclusiveBetween(10, 100);
        }
	}
}

