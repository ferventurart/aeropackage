using System;
using Ardalis.SmartEnum;

namespace AeroPackage.Domain.PackageAggregate.Enums;

public class PackageStatus : SmartEnum<PackageStatus>
{
    public PackageStatus(string name, int value) : base(name, value)
    {
    }

    public static readonly PackageStatus PreAlert = new(nameof(PreAlert), 0);
    public static readonly PackageStatus ReceivedInUsa = new(nameof(ReceivedInUsa), 1);
    public static readonly PackageStatus PreparingForShipment = new(nameof(PreparingForShipment), 2);
    public static readonly PackageStatus InTransitToDestinationCountry = new(nameof(InTransitToDestinationCountry), 3);
    public static readonly PackageStatus ReceivedAtDestinationCountry = new(nameof(ReceivedAtDestinationCountry), 4);
    public static readonly PackageStatus CustomsClearanceInProcess = new(nameof(CustomsClearanceInProcess), 5);
    public static readonly PackageStatus ReleasedFromCustoms = new(nameof(ReleasedFromCustoms), 6);
    public static readonly PackageStatus StoredInWarehouse = new(nameof(StoredInWarehouse), 7);
    public static readonly PackageStatus DeliveredToLocalCourier = new(nameof(DeliveredToLocalCourier), 8);
    public static readonly PackageStatus Delivered = new(nameof(Delivered), 9);
}
