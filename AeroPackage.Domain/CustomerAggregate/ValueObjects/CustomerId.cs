using System;
using AeroPackage.Domain.Common.Models;
using AeroPackage.Domain.UserAggregate.ValueObjects;

namespace AeroPackage.Domain.CustomerAggregate.ValueObjects;

public sealed class CustomerId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private CustomerId(Guid value)
    {
        Value = value;
    }

    public static CustomerId CreateUnique()
    {
        return new CustomerId(Guid.NewGuid());
    }

    public static CustomerId Create(Guid customerId)
    {
        return new CustomerId(customerId);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    #pragma warning disable CS8618
    private CustomerId()
    {
    }
    #pragma warning restore CS8618
}