using System;
using System.Text.RegularExpressions;
using AeroPackage.Domain.Common.Models;

namespace AeroPackage.Domain.Common.ValueObjects;

public class NationalIdentification : ValueObject
{
	public NationalIdentification(string value)
	{
		Value = value;
	}

    public string Value { get; }

    public static int MaxLength => 10;

    public static Regex Pattern => new Regex(@"^\d{8}-\d{1}$");

    public static NationalIdentification Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException("Identification is required.");
        }

        if (value.Length > MaxLength)
        {
            throw new ArgumentOutOfRangeException("Identification only allows 10 characters.");
        }

        if (!Pattern.IsMatch(value))
        {
            throw new FormatException($"{value} has not valid format.");
        }

        return new NationalIdentification(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

