using System;
using AeroPackage.Domain.CustomerAggregate.ValueObjects;
using AeroPackage.Domain.PackageAggregate;
using AeroPackage.Domain.UserAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Packages.Commands.CreatePackage;

public record CreatePackageCommand(
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

