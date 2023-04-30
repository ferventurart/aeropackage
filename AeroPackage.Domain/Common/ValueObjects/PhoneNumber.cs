using System;
using System.Text.RegularExpressions;
using AeroPackage.Domain.Common.Models;

namespace AeroPackage.Domain.Common.ValueObjects;

public sealed class PhoneNumber : ValueObject
{
	public PhoneNumber(string value)
	{
		Value = value;
	}

	public string Value { get; }

	public static int MaxLength => 9;

	public static Regex Pattern => new Regex(@"^(?:-*\d-*){8}$");

	public static PhoneNumber Create(string value)
	{
		if(string.IsNullOrEmpty(value))
		{
			throw new ArgumentNullException("Phone Number is required.");
		}

		if(value.Length > MaxLength)
		{
			throw new ArgumentOutOfRangeException("Phone Number only allows 9 characters.");
		}

		if(!Pattern.IsMatch(value))
		{
			throw new FormatException($"{value} has not valid format.");
		}

		return new PhoneNumber(value);
	}

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

