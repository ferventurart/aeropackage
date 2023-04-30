using System;
using Microsoft.AspNetCore.Http;

namespace AeroPackage.Contracts.Package;

public record CreatePackageRequest(
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
    List<IFormFile>? Attachments);

