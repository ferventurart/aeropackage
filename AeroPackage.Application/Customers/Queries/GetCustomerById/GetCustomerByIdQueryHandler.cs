using AeroPackage.Domain.Common.DomainErrors;
using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Application.Customers.Commands.CreateCustomer;
using AeroPackage.Domain.CustomerAggregate;
using AeroPackage.Domain.CustomerAggregate.ValueObjects;
using ErrorOr;
using MediatR;


namespace AeroPackage.Application.Customers.Queries.GetCustomerById;

public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, ErrorOr<Customer>>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerByIdHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<ErrorOr<Customer>> Handle(GetCustomerByIdQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var customer = _customerRepository.GetCustomerById(CustomerId.Create(query.Id));

        if (customer is null)
        {
            return Errors.Customer.NotFound;
        }

        return customer;
    }
}

