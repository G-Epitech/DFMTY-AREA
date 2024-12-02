using ErrorOr;

namespace Zeus.Api.Domain.Authentication;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error DuplicatedEmail => Error.Conflict(
            code: "Auth.DuplicatedEmail",
            description: "Email already used."
        );

        public static Error InvalidCredentials => Error.Unauthorized(
            code: "Auth.InvalidCredentials",
            description: "Invalid credentials."
        );
    }
}
