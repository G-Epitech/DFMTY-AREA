using ErrorOr;

namespace Zeus.Api.Domain.UserAggregate;

public static partial class Errors
{
    public static class User
    {
        public static Error NotFound => Error.NotFound(
            code: "User.NotFound",
            description: "User not found."
        );
    }
}
