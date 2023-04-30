using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Application.Customers.Commands.CreateCustomer;
using AeroPackage.Domain.CustomerAggregate;
using AeroPackage.Domain.CustomerAggregate.ValueObjects;
using AeroPackage.Domain.Common.DomainErrors;
using ErrorOr;
using MediatR;
using System;
using AeroPackage.Domain.Common.ValueObjects;
using AeroPackage.Domain.CustomerAggregate.Enums;

namespace AeroPackage.Application.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, ErrorOr<Customer>>
{
    private readonly ICustomerRepository _customerRepository;

    public UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<ErrorOr<Customer>> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_customerRepository.GetCustomerById(CustomerId.Create(command.Id)) is not Customer customer)
        {
            return Errors.Customer.NotFound;
        }

        customer.UpdateProperty(c => c.FirstName, command.FirstName);
        customer.UpdateProperty(c => c.LastName,  command.LastName);
        customer.UpdateProperty(c => c.Email, command.Email);
        customer.UpdateProperty(c => c.CellPhoneNumber, PhoneNumber.Create(command.CellPhoneNumber));
        customer.UpdateProperty(c => c.BirthDate, command.BirthDate);
        customer.UpdateProperty(c => c.Gender, Gender.FromValue(command.Gender));
        customer.UpdateProperty(c => c.Identification, NationalIdentification.Create(command.Identification));
        customer.UpdateProperty(c => c.Address, command.Address);
        customer.UpdateProperty(c => c.Status, CustomerStatus.FromValue(command.Status));
        customer.UpdateProperty(c => c.UpdatedDateTime, DateTime.Now);

        _customerRepository.Update(customer);

        return customer;
    }
}

