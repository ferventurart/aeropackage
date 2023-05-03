using System;
using AeroPackage.Domain.Common.Models;
using AeroPackage.Domain.PackageAggregate.ValueObjects;
using AeroPackage.Domain.SaleAggregate.ValueObjects;

namespace AeroPackage.Domain.SaleAggregate.Entities;

public sealed class SaleItem : Entity<SaleItemId>
{
    public PackageId? PackageId { get; private set; }
    public string Description { get; private set; }
    public int Quantity { get; private set; }
    public decimal Rate { get; private set; }
    public decimal LineTotal { get; set; }

    private SaleItem(PackageId? packageId, string description, int quantity,
    decimal rate, decimal lineTotal) : base(SaleItemId.CreateUnique())
    {
        PackageId = packageId;
        Description = description;
        Quantity = quantity;
        Rate = rate;
        LineTotal = lineTotal;
    }

    public static SaleItem Create(PackageId? packageId, string description,
        int quantity, decimal rate)
    {
        var lineTotal = quantity * rate;
        return new SaleItem(packageId, description, quantity, rate, lineTotal);
    }
}

