using System;
using AeroPackage.Domain.UserAggregate;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Users.Commands.UpdateProfile;

public record UpdateProfileCommand(Guid Id, string FirstName, string LastName, string? Password, string? ConfirmPassword) : IRequest<ErrorOr<User>>;

