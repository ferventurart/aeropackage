using System;
using AeroPackage.Application.Authentication.Queries.Login;
using FluentValidation;

namespace AeroPackage.Application.Authentication.Queries.Verify;

public class VerifyTokenQueryValidator : AbstractValidator<VerifyTokenQuery>
{
    public VerifyTokenQueryValidator()
    {
        RuleFor(x => x.Token).NotEmpty();
    }
}

