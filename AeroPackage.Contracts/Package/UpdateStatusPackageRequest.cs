using System;
namespace AeroPackage.Contracts.Package;

public record UpdateStatusPackageRequest(string OwnTrackingNumber, string Status);

