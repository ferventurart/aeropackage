using System;
using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Application.Customers.Queries.GetCustomers;
using AeroPackage.Domain.CustomerAggregate;
using AeroPackage.Domain.CustomerAggregate.Enums;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Customers.Queries.GetCustomersActiveByName;

public class GetCustomersActiveByNameQueryHandler : IRequestHandler<GetCustomersActiveByNameQuery, ErrorOr<List<Customer>>>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomersActiveByNameQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<ErrorOr<List<Customer>>> Handle(GetCustomersActiveByNameQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        return await _customerRepository.GetCustomersActiveByName(query.Name);
    }
}

