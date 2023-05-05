using System;
using AeroPackage.Domain.Common.Models;

namespace AeroPackage.Domain.ServicesAggregate.ValueObjects;

public sealed class ServiceId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private ServiceId(Guid value)
    {
        Value = value;
    }

    public static ServiceId CreateUnique()
    {
        return new ServiceId(Guid.NewGuid());
    }

    public static ServiceId Create(Guid ServiceId)
    {
        return new ServiceId(ServiceId);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    #pragma warning disable CS8618
    private ServiceId()
    {
    }
    #pragma warning restore CS8618
}

