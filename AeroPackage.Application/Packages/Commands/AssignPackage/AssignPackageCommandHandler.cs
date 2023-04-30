using System;
using AeroPackage.Domain.Common.DomainErrors;
using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Domain.CustomerAggregate.ValueObjects;
using AeroPackage.Domain.PackageAggregate;
using AeroPackage.Domain.PackageAggregate.ValueObjects;
using ErrorOr;
using MediatR;
using AeroPackage.Domain.CustomerAggregate;
using AeroPackage.Application.Packages.Events;

namespace AeroPackage.Application.Customers.Commands.AssignPackage;

public class AssignPackageCommandHandler : INotificationHandler<NewPackageRegisteredEvent>
{
    private readonly IPackageRepository _packageRepository;
    private readonly ICustomerRepository _customerRepository;

    public AssignPackageCommandHandler(IPackageRepository packageRepository, ICustomerRepository customerRepository)
    {
        _packageRepository = packageRepository;
        _customerRepository = customerRepository;
    }

    public async Task Handle(NewPackageRegisteredEvent notification, CancellationToken cancellationToken)
    {
        var packageId = PackageId.Create(notification.PackageId);
        var customerId = CustomerId.Create(notification.CustomerId);

        if (_packageRepository.GetPackageById(packageId) is Package package && _customerRepository.GetCustomerById(customerId) is Customer customer)
        {
            customer.AssignPackageToCustomer(packageId);

            _customerRepository.Update(customer);
        }
    }

}

