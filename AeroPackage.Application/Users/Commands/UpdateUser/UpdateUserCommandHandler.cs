using System;
using AeroPackage.Domain.Common.DomainErrors;
using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Domain.Common.ValueObjects;
using AeroPackage.Domain.UserAggregate;
using AeroPackage.Domain.UserAggregate.ValueObjects;
using ErrorOr;
using MediatR;
using AeroPackage.Domain.UserAggregate.Enums;

namespace AeroPackage.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ErrorOr<User>>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<User>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_userRepository.GetUserById(UserId.Create(command.Id)) is not User user)
        {
            return Errors.User.NotFound;
        }

        user.UpdateProperty(c => c.FirstName, command.FirstName);
        user.UpdateProperty(c => c.LastName, command.LastName);
        user.UpdateProperty(c => c.Email, command.Email);
        user.UpdateProperty(c => c.Role, UserRole.FromValue(command.Role));
        user.UpdateProperty(c => c.Status, UserStatus.FromValue(command.Status));
        user.UpdateProperty(c => c.UpdatedDateTime, DateTime.Now);

        _userRepository.Update(user);

        return user;
    }
}

