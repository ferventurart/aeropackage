using System;
namespace AeroPackage.Contracts.Package;

public record DragPackageResponse(
    string OwnTrackingNumber,
    string Description,
    string Store,
    string Status);

