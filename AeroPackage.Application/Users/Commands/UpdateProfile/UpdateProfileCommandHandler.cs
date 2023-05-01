using AeroPackage.Domain.Common.DomainErrors;
using AeroPackage.Domain.UserAggregate;
using ErrorOr;
using System;
using MediatR;
using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Domain.UserAggregate.Enums;
using AeroPackage.Domain.UserAggregate.ValueObjects;
using BC = BCrypt.Net.BCrypt;

namespace AeroPackage.Application.Users.Commands.UpdateProfile;

public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, ErrorOr<User>>
{
    private readonly IUserRepository _userRepository;

    public UpdateProfileCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<User>> Handle(UpdateProfileCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_userRepository.GetUserById(UserId.Create(command.Id)) is not User user)
        {
            return Errors.User.NotFound;
        }

        user.UpdateProperty(c => c.FirstName, command.FirstName);
        user.UpdateProperty(c => c.LastName, command.LastName);
        if (!string.IsNullOrEmpty(command.Password) && !string.IsNullOrEmpty(command.ConfirmPassword) && command.Password == command.ConfirmPassword)
        {
            var hashPassword = BC.HashPassword(command.Password);

            if(BC.Verify(command.Password, hashPassword))
            {
                user.UpdateProperty(c => c.Password, hashPassword);
            }
           
        }
        user.UpdateProperty(c => c.UpdatedDateTime, DateTime.Now);

        _userRepository.Update(user);

        return user;
    }
}

