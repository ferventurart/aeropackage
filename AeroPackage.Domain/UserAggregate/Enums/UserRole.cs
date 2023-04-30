using System;
using Ardalis.SmartEnum;

namespace AeroPackage.Domain.UserAggregate.Enums;

public class UserRole : SmartEnum<UserRole>
{
    public UserRole(string name, int value) : base(name, value)
    {
    }

    public static readonly UserRole Admin = new(nameof(Admin), 1);
    public static readonly UserRole Executive = new(nameof(Executive), 2);
    public static readonly UserRole Warehouse = new(nameof(Warehouse), 3);
}

