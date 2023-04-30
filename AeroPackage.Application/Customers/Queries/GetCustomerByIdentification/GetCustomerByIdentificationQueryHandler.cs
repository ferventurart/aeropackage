using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Domain.Common.ValueObjects;
using AeroPackage.Domain.CustomerAggregate;
using AeroPackage.Domain.Common.DomainErrors;
using ErrorOr;
using MediatR;
using System;

namespace AeroPackage.Application.Customers.Queries.GetCustomerByIdentification;

public class GetCustomerByIdentificationQueryHandler : IRequestHandler<GetCustomerByIdentificationQuery, ErrorOr<Customer>>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerByIdentificationQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<ErrorOr<Customer>> Handle(GetCustomerByIdentificationQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var customer = _customerRepository.GetCustomerByIdentification(NationalIdentification.Create(query.Identification));

        if (customer is null)
        {
            return Errors.Customer.NotFound;
        }

        return customer;
    }
}

