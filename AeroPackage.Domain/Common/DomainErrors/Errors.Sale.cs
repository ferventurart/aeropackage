using System;
using ErrorOr;

namespace AeroPackage.Domain.Common.DomainErrors;

public static partial class Errors
{
    public static class Sale
    {
        public static Error NoSaleItems => Error.Conflict(
            code: "Sale.NoSaleItems",
            description: "The Sale have not details items.");
    }
}

