using ErrorOr;

namespace AeroPackage.Domain.Common.DomainErrors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict(
            code: "User.DuplicateEmail",
            description: "Email is already in use.");


        public static Error NotFound => Error.NotFound(
           code: "User.NotFound",
           description: "User not found");
    }
}