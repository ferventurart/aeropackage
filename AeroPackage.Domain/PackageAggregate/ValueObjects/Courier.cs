using System;
using System.Net;
using AeroPackage.Domain.Common.Models;

namespace AeroPackage.Domain.PackageAggregate.ValueObjects;

public class Courier : ValueObject
{
    public string Name { get; private set; }
    public string UrlLogo { get; private set; }

    private Courier(string name, string urlLogo)
    {
        Name = name;
        UrlLogo = urlLogo;
    }

    public static Courier Create(string name, string urlLogo)
    {
        // TODO: Enforce invariants
        return new Courier(name, urlLogo);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return UrlLogo;
    }

    #pragma warning disable CS8618
    private Courier()
    {
    }
    #pragma warning restore CS8618
}

