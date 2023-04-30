using System;
using AeroPackage.Domain.Common.DomainErrors;
using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Application.Customers.Queries.GetCustomerById;
using AeroPackage.Domain.CustomerAggregate.ValueObjects;
using AeroPackage.Domain.UserAggregate;
using AeroPackage.Domain.UserAggregate.ValueObjects;
using ErrorOr;
using MediatR;


namespace AeroPackage.Application.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ErrorOr<User>>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<User>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var customer = _userRepository.GetUserById(UserId.Create(query.Id));

        if (customer is null)
        {
            return Errors.Customer.NotFound;
        }

        return customer;
    }
}

