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
    }
}
