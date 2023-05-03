using System;
using AeroPackage.Domain.Common.Models;

namespace AeroPackage.Domain.SaleAggregate.ValueObjects;

public sealed class SaleItemId : ValueObject
{
    public Guid Value { get; private set; }

    private SaleItemId(Guid value)
    {
        Value = value;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static SaleItemId Create(Guid value)
    {
        return new SaleItemId(value);
    }

    public static SaleItemId CreateUnique()
    {
        return new SaleItemId(Guid.NewGuid());
    }

    #pragma warning disable CS8618
    private SaleItemId()
    {
    }
    #pragma warning restore CS8618
}

