using System;
namespace AeroPackage.Contracts.Package;

public record PackageHistoryResponse(
    Guid Id,
    DateTime DateMovement,
    string Status);
