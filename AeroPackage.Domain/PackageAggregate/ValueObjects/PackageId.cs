using System;
using AeroPackage.Domain.Common.Models;

namespace AeroPackage.Domain.PackageAggregate.ValueObjects;

public sealed class PackageId : AggregateRootId<int>
{
    public override int Value { get; protected set; }

    private PackageId(int value)
    {
        Value = value;
    }

    public static PackageId Create(int packageId)
    {
        return new PackageId(packageId);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    #pragma warning disable CS8618
    private PackageId()
    {
    }
    #pragma warning restore CS8618
}
