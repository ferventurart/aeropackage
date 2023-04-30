using System;
namespace AeroPackage.Contracts.Package;

public record PackageTimeLineResponse(
    Guid Id,
    DateTime DateMovement,
    string Status);
