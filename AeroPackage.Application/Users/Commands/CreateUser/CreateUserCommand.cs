using System;
using AeroPackage.Domain.UserAggregate;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Users.Commands.CreateUser;

public record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    int Role) : IRequest<ErrorOr<User>>;

