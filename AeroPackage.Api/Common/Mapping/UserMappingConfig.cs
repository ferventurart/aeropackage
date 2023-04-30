using System;
using AeroPackage.Application.Users.Commands.DeleteUser;
using AeroPackage.Application.Users.Commands.UpdateUser;
using AeroPackage.Contracts.User;
using AeroPackage.Domain.UserAggregate;
using Mapster;

namespace AeroPackage.Api.Common.Mapping;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UpdateUserRequest, UpdateUserCommand>();
        config.NewConfig<DeleteUserRequest, DeleteUserCommand>();

        config.NewConfig<User, UserResponse>()
              .Map(dest => dest.Id, src => src.Id.Value.ToString())
              .Map(dest => dest.Role, src => src.Role.Value)
              .Map(dest => dest.Status, src => src.Status.Value);
    }
}

