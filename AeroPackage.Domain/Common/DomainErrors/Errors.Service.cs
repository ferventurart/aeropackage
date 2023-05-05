using ErrorOr;

namespace AeroPackage.Domain.Common.DomainErrors;

public static partial class Errors
{
    public static class Service
    {
        public static Error NotFound => Error.NotFound(
           code: "Service.NotFound",
           description: "Service not found");
    }
}

