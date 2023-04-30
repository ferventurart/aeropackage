using System;
using AeroPackage.Domain.Common.Models;

namespace AeroPackage.Domain.PackageAggregate.ValueObjects;

public sealed class PackageHistoryId : ValueObject
{
    public Guid Value { get; private set; }

    private PackageHistoryId(Guid value)
    {
        Value = value;
    }

    public static PackageHistoryId CreateUnique()
    {
        return new PackageHistoryId(Guid.NewGuid());
    }

    public static PackageHistoryId Create(Guid PackageHistoryId)
    {
        return new PackageHistoryId(PackageHistoryId);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    #pragma warning disable CS8618
    private PackageHistoryId()
    {
    }
    #pragma warning restore CS8618
}

