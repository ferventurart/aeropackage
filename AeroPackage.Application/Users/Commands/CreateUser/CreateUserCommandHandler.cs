using System;
using AeroPackage.Domain.Common.DomainErrors;
using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Application.Users.Commands.CreateUser;
using AeroPackage.Domain.Common.ValueObjects;
using AeroPackage.Domain.UserAggregate;
using AeroPackage.Domain.UserAggregate.Enums;
using BC = BCrypt.Net.BCrypt;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ErrorOr<User>>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<User>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_userRepository.GetUserByEmail(command.Email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }

        var user = User.Create(
            command.FirstName,
            command.LastName,
            command.Email,
            BC.HashPassword(command.Password),
            (UserRole) command.Role);

        user.UpdateProperty(c => c.CreatedDateTime, DateTime.Now);

        _userRepository.Add(user);

        return user;
    }
}

