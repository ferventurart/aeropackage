using ErrorOr;

namespace AeroPackage.Domain.Common.DomainErrors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidCredentials => Error.Validation(
            code: "Auth.InvalidCred",
            description: "Credenciales Invalidas.");

        public static Error InvalidToken => Error.Validation(
            code: "Auth.InvalidToken",
            description: "Token No Valido.");
    }
}