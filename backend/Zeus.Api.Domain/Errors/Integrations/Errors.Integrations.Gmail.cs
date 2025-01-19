using ErrorOr;

namespace Zeus.Api.Domain.Errors.Integrations;

public static partial class Errors
{
    public static partial class Integrations
    {
        public static class Gmail
        {
            public static Error ErrorDuringRequest => Error.Failure(
                code: "Integrations.Gmail.ErrorDuringRequest",
                description: "Error during request to Gmail API."
            );

            public static Error InvalidBody => Error.Validation(
                code: "Integrations.Gmail.InvalidBody",
                description: "Invalid body."
            );

            public static Error InvalidLinkRequest => Error.Validation(
                code: "Integrations.Gmail.InvalidLinkRequest",
                description: "Invalid link request."
            );
        }
    }
}
