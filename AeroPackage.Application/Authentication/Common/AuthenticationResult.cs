using AeroPackage.Domain.UserAggregate;

namespace AeroPackage.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token);