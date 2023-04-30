using System;
using AeroPackage.Domain.Common.DomainErrors;
using AeroPackage.Application.Common.Models;
using AeroPackage.Domain.UserAggregate;
using ErrorOr;
using MediatR;
using AeroPackage.Application.Common.Interfaces.Persistence;

namespace AeroPackage.Application.Users.Queries.GetUsers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ErrorOr<PaginatedResult<User>>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<PaginatedResult<User>>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        return await _userRepository.GetAll(query.PageSize, query.PageNumber);
    }
}

