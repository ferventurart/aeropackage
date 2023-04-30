using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Domain.Common.DomainErrors;
using AeroPackage.Domain.Common.ValueObjects;
using AeroPackage.Domain.CustomerAggregate;
using AeroPackage.Domain.CustomerAggregate.Enums;
using AeroPackage.Domain.CustomerAggregate.ValueObjects;
using ErrorOr;
using MediatR;
using System;

namespace AeroPackage.Application.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ErrorOr<Customer>>
{
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<ErrorOr<Customer>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        // 1. Validate the customer doesn't exist
        var customerNationalIdentification = NationalIdentification.Create(command.Identification);

        if (_customerRepository.GetCustomerByIdentification(customerNationalIdentification) is not null)
        {
            return Errors.Customer.DuplicateIdentification;
        }

        if (_customerRepository.GetCustomerByEmail(command.Email) is not null)
        {
            return Errors.Customer.DuplicateEmail;
        }

        var customer = Customer.Create(
            command.FirstName,
            command.LastName,
            command.Email,
            PhoneNumber.Create(command.CellPhoneNumber),
            command.BirthDate,
            (Gender)command.Gender,
            customerNationalIdentification,
            command.Address);

        customer.UpdateProperty(c => c.CreatedDateTime, DateTime.Now);

        _customerRepository.Add(customer);

        return customer;
    }
}

