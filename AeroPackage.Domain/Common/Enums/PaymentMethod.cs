using System;
using Ardalis.SmartEnum;

namespace AeroPackage.Domain.Common.Enums;

public class PaymentMethod : SmartEnum<PaymentMethod>
{
    public PaymentMethod(string name, int value) : base(name, value)
    {
    }

    public static readonly PaymentMethod Cash = new(nameof(Cash), 1);
    public static readonly PaymentMethod DebitCard = new(nameof(DebitCard), 2);
    public static readonly PaymentMethod CreditCard = new(nameof(CreditCard), 3);
    public static readonly PaymentMethod Transfer = new(nameof(Transfer), 4);
    public static readonly PaymentMethod PayPal = new(nameof(PayPal), 5);
}

