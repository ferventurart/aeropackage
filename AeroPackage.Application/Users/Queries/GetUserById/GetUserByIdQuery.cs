using System;
using AeroPackage.Domain.UserAggregate;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Users.Queries.GetUserById;

public record GetUserByIdQuery(Guid Id) : IRequest<ErrorOr<User>>;

