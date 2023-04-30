using System;
using AeroPackage.Domain.PackageAggregate;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Packages.Commands.UpdatePackage;

public record UpdatePackageCommand(
    int Id,
    Guid UserId,
    Guid CustomerId,
    string Store,
    string? Courier,
    string? CourierTrackingNumber,
    decimal Weight,
    int QuantityArticles,
    string Description,
    decimal DeclaredValue,
    List<string> Attachments) : IRequest<ErrorOr<Package>>;