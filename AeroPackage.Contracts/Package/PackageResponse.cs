using System;
namespace AeroPackage.Contracts.Package;

public record PackageResponse(
    int Id,
    string OwnTrackingNumber,
    string UserId,
    string CustomerId,
    string Consignee,
    string Store,
    string? Courier,
    string? CourierTrackingNumber,
    decimal Weight,
    int QuantityArticles,
    string Description,
    decimal DeclaredValue,
    decimal? TaxValue,
    int Status,
    string CreatedBy,
    DateTime CreatedDateTime,
    List<string> Attachments,
    List<PackageHistoryResponse> PackageHistories);

