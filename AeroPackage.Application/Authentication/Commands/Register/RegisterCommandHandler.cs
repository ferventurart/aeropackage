using AeroPackage.Application.Authentication.Common;
using AeroPackage.Application.Common.Interfaces.Authentication;
using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Domain.Common.DomainErrors;
using AeroPackage.Domain.UserAggregate;
using BC = BCrypt.Net.BCrypt;
using ErrorOr;

using MediatR;
using AeroPackage.Domain.UserAggregate.Enums;

namespace AeroPackage.Application.Authentication.Commands.Register;

public class RegisterCommandHandler :
    IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        // 1. Validate the user doesn't exist
        if (_userRepository.GetUserByEmail(command.Email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }

        // 2. Create user (generate unique ID) & Persist to DB
        var user = User.Create(command.FirstName, command.LastName, command.Email, BC.HashPassword(command.Password), UserRole.Admin);

        user.UpdateProperty(c => c.CreatedDateTime, DateTime.UtcNow);

        _userRepository.Add(user);

        // 3. Create JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }
}