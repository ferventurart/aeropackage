using System;
using AeroPackage.Domain.Common.DomainErrors;
using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Application.Users.Commands.DeleteUser;
using AeroPackage.Domain.UserAggregate;
using AeroPackage.Domain.UserAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ErrorOr<User>>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<User>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var user = _userRepository.GetUserById(UserId.Create(command.Id));

        if (user is null)
        {
            return Errors.User.NotFound;
        }

        _userRepository.Delete(user);

        return user;
    }
}

