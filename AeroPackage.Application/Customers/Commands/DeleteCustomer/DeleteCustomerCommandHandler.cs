using AeroPackage.Domain.Common.DomainErrors;
using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Application.Customers.Commands.CreateCustomer;
using AeroPackage.Domain.Common.ValueObjects;
using AeroPackage.Domain.CustomerAggregate;
using AeroPackage.Domain.CustomerAggregate.Enums;
using AeroPackage.Domain.CustomerAggregate.ValueObjects;
using ErrorOr;
using MediatR;
using System;

namespace AeroPackage.Application.Customers.Commands.DeleteCustomer;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, ErrorOr<Customer>>
{
    private readonly ICustomerRepository _customerRepository;

    public DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<ErrorOr<Customer>> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var customer = _customerRepository.GetCustomerById(CustomerId.Create(command.Id));

        if(customer is null)
        {
            return Errors.Customer.NotFound;
        }

        _customerRepository.Delete(customer);

        return customer;
    }
}

