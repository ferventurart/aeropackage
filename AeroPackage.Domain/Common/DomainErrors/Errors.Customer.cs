using ErrorOr;

namespace AeroPackage.Domain.Common.DomainErrors;

public static partial class Errors
{
    public static class Customer
	{
        public static Error DuplicateIdentification => Error.Conflict(
           code: "Customer.DuplicateIdentification",
           description: "Identification is already in use.");

        public static Error DuplicateEmail => Error.Conflict(
           code: "Customer.DuplicateEmail",
           description: "Email is already in use.");

        public static Error NotFound => Error.NotFound(
           code: "Customer.NotFound",
           description: "Customer not found");
    }
}

