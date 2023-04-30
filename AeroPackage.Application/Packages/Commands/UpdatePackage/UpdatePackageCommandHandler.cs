using AeroPackage.Domain.Common.DomainErrors;
using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Application.Packages.Commands.CreatePackage;
using AeroPackage.Application.Packages.Events;
using AeroPackage.Domain.CustomerAggregate;
using AeroPackage.Domain.CustomerAggregate.ValueObjects;
using AeroPackage.Domain.PackageAggregate;
using AeroPackage.Domain.PackageAggregate.Enums;
using AeroPackage.Domain.PackageAggregate.ValueObjects;
using AeroPackage.Domain.UserAggregate;
using AeroPackage.Domain.UserAggregate.ValueObjects;
using ErrorOr;
using MediatR;
using System;
using AeroPackage.Domain.Common.ValueObjects;
using AeroPackage.Domain.CustomerAggregate.Enums;

namespace AeroPackage.Application.Packages.Commands.UpdatePackage;

public class UpdatePackageCommandHandler : IRequestHandler<UpdatePackageCommand, ErrorOr<Package>>
{
    private readonly IPackageRepository _packageRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMediator _mediator;

    public UpdatePackageCommandHandler(IPackageRepository packageRepository, ICustomerRepository customerRepository, IUserRepository userRepository, IMediator mediator)
    {
        _packageRepository = packageRepository;
        _customerRepository = customerRepository;
        _userRepository = userRepository;
        _mediator = mediator;
    }

    public async Task<ErrorOr<Package>> Handle(UpdatePackageCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var customerId = CustomerId.Create(command.CustomerId);
        var userId = UserId.Create(command.UserId);
        var packageId = PackageId.Create(command.Id);

        if (_packageRepository.GetPackageById(packageId) is not Package package)
        {
            return Errors.Package.NotFound;
        }

        if (_customerRepository.GetCustomerById(customerId) is not Customer customer)
        {
            return Errors.Customer.NotFound;
        }

        if (_userRepository.GetUserById(userId) is not User user)
        {
            return Errors.User.NotFound;
        }

        if (command.CourierTrackingNumber != package.CourierTrackingNumber)
        {
            var exitingPackage = _packageRepository.GetPackageByTrackingNumber(command.CourierTrackingNumber);

            if (exitingPackage is not null && exitingPackage.Id != packageId)
            {
                return Errors.Package.DuplicateCourierTrackingNumber;
            }

        }

        package.UpdateProperty(p => p.CustomerId, customer.Id);
        package.UpdateProperty(p => p.Consignee, customer.GetCustomerFullName());
        package.UpdateProperty(p => p.Store, command.Store);
        package.UpdateProperty(p => p.Courier, command.Courier);
        package.UpdateProperty(p => p.CourierTrackingNumber, command.CourierTrackingNumber);
        package.UpdateProperty(p => p.Weight, command.Weight);
        package.UpdateProperty(p => p.QuantityArticles, command.QuantityArticles);
        package.UpdateProperty(p => p.Description, command.Description);
        package.UpdateProperty(p => p.DeclaredValue, command.DeclaredValue);
        package.UpdateProperty(p => p.UpdatedDateTime, DateTime.UtcNow);
        package.UdpateTaxValue(command.DeclaredValue);

        if (command.Attachments.Count > 0)
        {
            for (int i = 0; i < command.Attachments.Count; i++)
            {
                package.AddAttachment(command.Attachments[i]);
            }
        }

        _packageRepository.Update(package);

        return package;
    }
}
