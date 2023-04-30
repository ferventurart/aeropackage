using AeroPackage.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;