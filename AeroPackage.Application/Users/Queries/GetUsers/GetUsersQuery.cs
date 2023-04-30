using System;
using AeroPackage.Application.Common.Models;
using AeroPackage.Domain.UserAggregate;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Users.Queries.GetUsers;

public record GetUsersQuery(int PageSize, int PageNumber) : IRequest<ErrorOr<PaginatedResult<User>>>;

