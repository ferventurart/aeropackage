using System;
using AeroPackage.Application.Services.Queries.GetServicesActiveByName;
using FluentValidation;
namespace AeroPackage.Application.Services.Queries.GetServicesActiveByName;

public class GetServicesActiveByNameQueryValidator : AbstractValidator<GetServicesActiveByNameQuery>
{
    public GetServicesActiveByNameQueryValidator()
    {
        RuleFor(r => r.Name)
          .NotEmpty()
          .MaximumLength(60);
    }
}

