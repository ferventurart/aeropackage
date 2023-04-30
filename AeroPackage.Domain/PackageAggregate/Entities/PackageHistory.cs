using System;
using AeroPackage.Domain.Common.Models;
using AeroPackage.Domain.PackageAggregate.ValueObjects;

namespace AeroPackage.Domain.PackageAggregate.Entities;

public sealed class PackageHistory : Entity<PackageHistoryId>
{
    public DateTime DateMovement { get; private set; }

    public string Status { get; private set; }

    private PackageHistory(
        DateTime dateMovement,
        string status,
        PackageHistoryId? id = null) : base(id ?? PackageHistoryId.CreateUnique())
    {
        DateMovement = dateMovement;
        Status = status;
    }

    public static PackageHistory Create(string status)
    {
        return new PackageHistory(DateTime.UtcNow, status);
    }

    #pragma warning disable CS8618
    private PackageHistory()
    {
    }
    #pragma warning restore CS8618
}

