using ErrorOr;

namespace Zeus.Api.Domain.Errors.Integrations;

public static partial class Errors
{
    public static partial class Integrations
    {
        public static class Github
        {
            public static Error ErrorDuringTokenRequest => Error.Failure(
                code: "Integrations.Github.ErrorDuringTokenRequest",
                description: "Error during github token request."
            );

            public static Error ErrorDuringUserRequest => Error.Failure(
                code: "Integrations.Github.ErrorDuringUserRequest",
                description: "Error during github user request."
            );

            public static Error InvalidBody => Error.Validation(
                code: "Integrations.Github.InvalidBody",
                description: "Invalid body."
            );
        }
    }
}
