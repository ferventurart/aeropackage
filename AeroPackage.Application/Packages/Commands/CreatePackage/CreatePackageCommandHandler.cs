
using AeroPackage.Domain.Common.DomainErrors;
using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Application.Customers.Commands.CreateCustomer;
using AeroPackage.Domain.PackageAggregate;
using AeroPackage.Domain.PackageAggregate.Enums;
using AeroPackage.Domain.UserAggregate.ValueObjects;
using AeroPackage.Domain.CustomerAggregate.ValueObjects;
using AeroPackage.Domain.PackageAggregate.ValueObjects;
using AeroPackage.Domain.CustomerAggregate;
using AeroPackage.Domain.UserAggregate;
using AeroPackage.Application.Packages.Events;
using ErrorOr;
using MediatR;
using System;
using AeroPackage.Domain.PackageAggregate.Entities;

namespace AeroPackage.Application.Packages.Commands.CreatePackage;

public class CreatePackageCommandHandler : IRequestHandler<CreatePackageCommand, ErrorOr<Package>>
{
    private readonly IPackageRepository _packageRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMediator _mediator;

    public CreatePackageCommandHandler(IPackageRepository packageRepository, ICustomerRepository customerRepository, IUserRepository userRepository, IMediator mediator)
    {
        _packageRepository = packageRepository;
        _customerRepository = customerRepository;
        _userRepository = userRepository;
        _mediator = mediator;
    }

    public async Task<ErrorOr<Package>> Handle(CreatePackageCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var customerId = CustomerId.Create(command.CustomerId);
        var userId = UserId.Create(command.UserId);
        var packageId = PackageId.Create(0);

        var lastInsertedId = await _packageRepository.GetLastInsertedId();

        var ownTrackingNumber = OwnTrackingNumber.Create(lastInsertedId);

        if (_customerRepository.GetCustomerById(customerId) is not Customer customer)
        {
            return Errors.Customer.NotFound;
        }

        if(_userRepository.GetUserById(userId) is not User user)
        {
            return Errors.User.NotFound;
        }
        
        if (!string.IsNullOrEmpty(command.CourierTrackingNumber) && _packageRepository.ExistsWithTrackingNumber(command.CourierTrackingNumber))
        {
            return Errors.Package.DuplicateCourierTrackingNumber;
        }

        var package = Package.Create(
            ownTrackingNumber,
            userId,
            customerId,
            customer.GetCustomerFullName(),
            command.Store,
            command.Courier,
            command.CourierTrackingNumber,
            command.Weight,
            command.QuantityArticles,
            command.Description,
            command.DeclaredValue,
            null);

        if(command.Attachments.Count > 0)
        {
            for (int i = 0; i < command.Attachments.Count; i++)
            {
                package.AddAttachment(command.Attachments[i]);
            }
        }

        package.AddCreatedBy(user.GetUserFullName());

        package.UpdateProperty(p => p.CreatedDateTime, DateTime.UtcNow);

        package.AddHistoryToTimeLine(PackageStatus.PreAlert.Name);

        _packageRepository.Add(package);

        await _mediator.Publish(new NewPackageRegisteredEvent { CustomerId = customerId.Value, PackageId = packageId.Value });
        return package;
    }
}

