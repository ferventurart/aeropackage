using AeroPackage.Application.Authentication.Common;
using AeroPackage.Application.Authentication.Queries.Login;
using AeroPackage.Application.Common.Interfaces.Authentication;
using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Domain.Common.DomainErrors;
using AeroPackage.Domain.UserAggregate;
using BC = BCrypt.Net.BCrypt;
using ErrorOr;

using MediatR;

namespace AeroPackage.Application.Authentication.Commands.Login;

public class LoginQueryHandler :
    IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        // 1. Validate the user exists
        if (_userRepository.GetUserByEmail(query.Email) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        // 2. Validate the password is correct
        if (!BC.Verify(query.Password, user.Password))
        {
            return Errors.Authentication.InvalidCredentials;
        }

        // 3. Create JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }
}