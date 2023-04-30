using System;
namespace AeroPackage.Contracts.Package;

public record UpdatePackageRequest(
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
    decimal? TaxValue,
    int Status);


