using System;
using AeroPackage.Domain.PackageAggregate.ValueObjects;

namespace AeroPackage.Application.Common.Interfaces.Services;

public interface ICourierProvider
{
    Courier? ValidateTrackingNumber(string trackingNumber);
}

