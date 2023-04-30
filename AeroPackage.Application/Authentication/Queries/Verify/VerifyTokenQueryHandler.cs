using System;
using AeroPackage.Application.Authentication.Common;
using AeroPackage.Application.Authentication.Queries.Login;
using AeroPackage.Application.Common.Interfaces.Authentication;
using AeroPackage.Application.Common.Interfaces.Persistence;
using ErrorOr;
using MediatR;
using AeroPackage.Domain.Common.DomainErrors;
using AeroPackage.Domain.UserAggregate.ValueObjects;
using AeroPackage.Domain.UserAggregate;

namespace AeroPackage.Application.Authentication.Queries.Verify;

public class VerifyTokenQueryHandler :
    IRequestHandler<VerifyTokenQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public VerifyTokenQueryHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(VerifyTokenQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var userId = _jwtTokenGenerator.ValidateJwtToken(query.Token);

        if(userId is null)
        {
            return Errors.Authentication.InvalidToken;
        }

        if(_userRepository.GetUserById(UserId.Create(userId.Value)) is not User user)
        {
            return Errors.User.NotFound;
        }

        return new AuthenticationResult(
            user,
            query.Token);
    }
}
