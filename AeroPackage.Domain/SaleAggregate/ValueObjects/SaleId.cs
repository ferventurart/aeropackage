using System;
using AeroPackage.Domain.Common.Models;

namespace AeroSale.Domain.SaleAggregate.ValueObjects;

public sealed class SaleId : AggregateRootId<int>
{
    public override int Value { get; protected set; }

    private SaleId(int value)
    {
        Value = value;
    }

    public static SaleId Create(int SaleId)
    {
        return new SaleId(SaleId);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618
    private SaleId()
    {
    }
#pragma warning restore CS8618
}

