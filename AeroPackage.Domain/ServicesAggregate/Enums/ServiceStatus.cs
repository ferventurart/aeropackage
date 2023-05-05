using System;
using Ardalis.SmartEnum;

namespace AeroPackage.Domain.ServicesAggregate.Enums;

public class ServiceStatus : SmartEnum<ServiceStatus>
{
    public ServiceStatus(string name, int value) : base(name, value)
    {
    }

    public static readonly ServiceStatus Inactive = new(nameof(Inactive), 0);
    public static readonly ServiceStatus Active = new(nameof(Active), 1);
}


