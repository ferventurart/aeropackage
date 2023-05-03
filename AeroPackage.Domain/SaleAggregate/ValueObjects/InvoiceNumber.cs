using System;
using AeroPackage.Domain.Common.Models;

namespace AeroPackage.Domain.SaleAggregate.ValueObjects;

public sealed class InvoiceNumber : ValueObject
{
    public string Value { get; private set; }

    private InvoiceNumber(string value)
    {
        Value = value;
    }

    public static InvoiceNumber Create(string value)
    {
        return new InvoiceNumber(value);
    }

    public static InvoiceNumber Create(int number)
    {
        string paddedNumber = number.ToString().PadLeft(9, '0');

        string uniqueNumber = $"INV{paddedNumber}";

        return new InvoiceNumber(uniqueNumber);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    #pragma warning disable CS8618
    private InvoiceNumber()
    {
    }
    #pragma warning restore CS8618
}

