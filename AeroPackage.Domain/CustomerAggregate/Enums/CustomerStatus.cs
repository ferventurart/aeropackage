using System;
using Ardalis.SmartEnum;

namespace AeroPackage.Domain.CustomerAggregate.Enums;

public class CustomerStatus : SmartEnum<CustomerStatus>
{
    public CustomerStatus(string name, int value) : base(name, value)
    {
    }

    public static readonly CustomerStatus Inactive = new(nameof(Inactive), 0);
    public static readonly CustomerStatus Active = new(nameof(Active), 1);
}


