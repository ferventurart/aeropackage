using System;
using AeroPackage.Application.Common.Interfaces.Services;
using AeroPackage.Domain.PackageAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Packages.Queries.GetCourierByTrackingNumber;

public class GetCourierByTrcknNumberQueryHandler : IRequestHandler<GetCourierByTrcknNumberQuery, ErrorOr<Courier?>>
{
    private readonly ICourierProvider _courierProvider;

    public GetCourierByTrcknNumberQueryHandler(ICourierProvider courierProvider)
    {
        _courierProvider = courierProvider;
    }

    public async Task<ErrorOr<Courier?>> Handle(GetCourierByTrcknNumberQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        return _courierProvider.ValidateTrackingNumber(query.TrackingNumber);
    }
}

