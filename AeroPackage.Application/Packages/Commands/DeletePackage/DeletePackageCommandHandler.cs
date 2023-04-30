using AeroPackage.Domain.Common.DomainErrors;
using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Domain.CustomerAggregate;
using AeroPackage.Domain.CustomerAggregate.ValueObjects;
using AeroPackage.Domain.PackageAggregate;
using AeroPackage.Domain.PackageAggregate.Enums;
using AeroPackage.Domain.PackageAggregate.ValueObjects;
using AeroPackage.Domain.UserAggregate.ValueObjects;
using ErrorOr;
using MediatR;
using System;

namespace AeroPackage.Application.Packages.Commands.DeletePackage;

public class DeletePackageCommandHandler : IRequestHandler<DeletePackageCommand, ErrorOr<Package>>
{
    private readonly IPackageRepository _packageRepository;
    private readonly ICustomerRepository _customerRepository;

    public DeletePackageCommandHandler(IPackageRepository packageRepository, ICustomerRepository customerRepository)
    {
        _packageRepository = packageRepository;
        _customerRepository = customerRepository;
    }

    public async Task<ErrorOr<Package>> Handle(DeletePackageCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var packageId = PackageId.Create(command.Id);

        if (_packageRepository.GetPackageById(packageId) is not Package package)
        {
            return Errors.Package.NotFound;
        }

        if(_customerRepository.GetCustomerById(package.CustomerId) is not Customer customer)
        {
            return Errors.Customer.NotFound;
        }

        customer.RemovePackagetFromCustomer(packageId);

        _customerRepository.Update(customer);
        _packageRepository.Delete(package);

        return package;
    }
}

