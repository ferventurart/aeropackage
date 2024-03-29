﻿using System;
using AeroPackage.Domain.PackageAggregate.Enums;
using Ardalis.SmartEnum;

namespace AeroPackage.Domain.SaleAggregate.Enums;

public class SaleStatus : SmartEnum<SaleStatus>
{
    public SaleStatus(string name, int value) : base(name, value)
    {
    }
    public static readonly SaleStatus Pending = new(nameof(Pending), 1);
    public static readonly PackageStatus Paid = new(nameof(Paid), 2);
    public static readonly PackageStatus Cancelled = new(nameof(Cancelled), 3);
    public static readonly PackageStatus Refunded = new(nameof(Refunded), 4);
}

