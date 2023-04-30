using System;
using AeroPackage.Domain.UserAggregate;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid Id) : IRequest<ErrorOr<User>>;
