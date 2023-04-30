using System;
using AeroPackage.Domain.Common.DomainErrors;
using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Domain.PackageAggregate;
using AeroPackage.Domain.PackageAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Packages.Queries.GetPackageByOwnTracker;

internal sealed class GetPackageByOwnTrackerQueryHandler : IRequestHandler<GetPackageByOwnTrackerQuery, ErrorOr<Package>>
{
    private readonly IPackageRepository _packageRepository;

    public GetPackageByOwnTrackerQueryHandler(IPackageRepository packageRepository)
    {
        _packageRepository = packageRepository;
    }

    public async Task<ErrorOr<Package>> Handle(GetPackageByOwnTrackerQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var package =  _packageRepository.GetPackageByOwnTrackingNumber(OwnTrackingNumber.Create(query.TrackingNumber));

        if(package is null)
        {
           return Errors.Package.NotFound;
        }

        return package;
    }
}

