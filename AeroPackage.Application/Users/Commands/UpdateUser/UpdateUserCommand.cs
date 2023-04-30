using System;
using AeroPackage.Application.Authentication.Common;
using AeroPackage.Domain.UserAggregate;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    int Role,
    int Status) : IRequest<ErrorOr<User>>;

