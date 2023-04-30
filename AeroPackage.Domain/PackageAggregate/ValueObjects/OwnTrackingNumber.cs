using System;
using AeroPackage.Domain.Common.Models;

namespace AeroPackage.Domain.PackageAggregate.ValueObjects;

public sealed class OwnTrackingNumber : ValueObject
{
    public string Value { get; private set; }

    private OwnTrackingNumber(string value)
    {
        Value = value;
    }

    public static OwnTrackingNumber Create(string value)
    {
        return new OwnTrackingNumber(value);
    }

    public static OwnTrackingNumber Create(int number)
    {
        string paddedNumber = number.ToString().PadLeft(9, '0');

        string uniqueNumber = $"AESH{paddedNumber}";

        return new OwnTrackingNumber(uniqueNumber);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    #pragma warning disable CS8618
    private OwnTrackingNumber()
    {
    }
    #pragma warning restore CS8618
}

