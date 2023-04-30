using AeroPackage.Domain.Common.DomainErrors;
using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Domain.PackageAggregate;
using AeroPackage.Domain.PackageAggregate.ValueObjects;
using ErrorOr;
using MediatR;
using System;

namespace AeroPackage.Application.Packages.Commands.DeleteAttachment;

public class DeleteAttachmentCommandHandler : IRequestHandler<DeleteAttachmentCommand, ErrorOr<bool>>
{
    private readonly IPackageRepository _packageRepository;

    public DeleteAttachmentCommandHandler(IPackageRepository packageRepository)
    {
        _packageRepository = packageRepository;
    }

    public async Task<ErrorOr<bool>> Handle(DeleteAttachmentCommand command, CancellationToken cancellationToken)
    {
        var packageId = PackageId.Create(command.Id);

        if (_packageRepository.GetPackageById(packageId) is not Package package)
        {
            return Errors.Package.NotFound;
        }

        if (!package.Attachments.Contains(command.FileName))
        {
            return false;
        }

        package.RemoveAttachment(command.FileName);

        _packageRepository.Update(package);

        return true;
    }
}

