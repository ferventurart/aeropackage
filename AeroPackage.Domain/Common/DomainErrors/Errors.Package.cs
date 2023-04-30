using System;
using ErrorOr;

namespace AeroPackage.Domain.Common.DomainErrors;

public static partial class Errors
{
    public static class Package
    {
        public static Error DuplicateCourierTrackingNumber => Error.Conflict(
            code: "Package.DuplicateCourierTrackingNumber",
            description: "Courier Tracking Number already registered in other package.");

        public static Error NotFound => Error.NotFound(
           code: "Package.NotFound",
           description: "Package not found");
    }
}

