using AeroPackage.Domain.PackageAggregate.ValueObjects;
using AeroPackage.Application.Common.Interfaces.Persistence;
using ErrorOr;
using MediatR;
using System;
using AeroPackage.Domain.UserAggregate.ValueObjects;
using AeroPackage.Domain.PackageAggregate.Enums;
using AeroPackage.Application.Packages.Events;
using AeroPackage.Domain.PackageAggregate.Entities;
using System.Windows.Input;

namespace AeroPackage.Application.Packages.Commands.UpdatePackageStatus;

public class UpdatePackageStatusCommandHandler : IRequestHandler<UpdatePackageStatusCommand, ErrorOr<bool>>
{
    private readonly IPackageRepository _packageRepository;
    private readonly IMediator _mediator;

    public UpdatePackageStatusCommandHandler(IPackageRepository packageRepository, IMediator mediator)
    {
        _packageRepository = packageRepository;
        _mediator = mediator;
    }

    public async Task<ErrorOr<bool>> Handle(UpdatePackageStatusCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await Task.CompletedTask;

            var package = _packageRepository.GetPackageByOwnTrackingNumber(OwnTrackingNumber.Create(command.OwnTrackingNumber));

            if (package is null)
            {
                return false;
            }

            var newStatus = PackageStatus.FromName(command.Status);
            var currentStatus = package.Status;

            package.ChangePackageStatus(newStatus, currentStatus);

            package.UpdateProperty(p => p.UpdatedDateTime, DateTime.UtcNow);

            _packageRepository.Update(package);

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}

