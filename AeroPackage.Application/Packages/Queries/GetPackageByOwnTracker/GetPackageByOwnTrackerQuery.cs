using System;
using AeroPackage.Domain.PackageAggregate;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Packages.Queries.GetPackageByOwnTracker;

public record GetPackageByOwnTrackerQuery(string TrackingNumber) : IRequest<ErrorOr<Package>>;

