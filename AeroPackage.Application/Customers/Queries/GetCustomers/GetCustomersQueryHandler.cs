using System;
using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Application.Common.Models;
using AeroPackage.Application.Customers.Queries.GetCustomerById;
using AeroPackage.Domain.CustomerAggregate;
using AeroPackage.Domain.CustomerAggregate.Enums;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Customers.Queries.GetCustomers;

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, ErrorOr<PaginatedResult<Customer>>>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomersQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<ErrorOr<PaginatedResult<Customer>>> Handle(GetCustomersQuery query, CancellationToken cancellationToken)
    {
        return await _customerRepository.GetAll(query.PageSize, query.PageNumber, query.Status is not null ? CustomerStatus.FromValue(query.Status.Value) : null);
    }
}

