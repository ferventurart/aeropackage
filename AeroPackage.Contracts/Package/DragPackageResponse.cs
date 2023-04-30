using System;
namespace AeroPackage.Contracts.Package;

public record DragPackageResponse(
    string OwnTrackingNumber,
    string Description,
    int QuantityArticles,
    string Store,
    string Status);

