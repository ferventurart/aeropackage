using System;
using Ardalis.SmartEnum;

namespace AeroPackage.Domain.UserAggregate.Enums;

public class UserStatus : SmartEnum<UserStatus>
{
    public UserStatus(string name, int value) : base(name, value)
    {
    }

    public static readonly UserStatus Inactive = new(nameof(Inactive), 0);
    public static readonly UserStatus Active = new(nameof(Active), 1);
}


