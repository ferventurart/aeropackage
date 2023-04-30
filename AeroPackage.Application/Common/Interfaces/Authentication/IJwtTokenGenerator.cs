using AeroPackage.Domain.UserAggregate;

namespace AeroPackage.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
    Guid? ValidateJwtToken(string? token);
}