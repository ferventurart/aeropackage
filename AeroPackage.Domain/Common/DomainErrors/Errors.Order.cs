using System;
using ErrorOr;

namespace AeroPackage.Domain.Common.DomainErrors;

public static partial class Errors
{
    public static class Order
    {
        public static Error InvalidOrderId => Error.Validation(
            code: "Order.InvalidId",
            description: "Order ID is invalid");
    }
}

