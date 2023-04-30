using System;
using AeroPackage.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Authentication.Queries.Verify;

public record VerifyTokenQuery(string Token) : IRequest<ErrorOr<AuthenticationResult>>;
