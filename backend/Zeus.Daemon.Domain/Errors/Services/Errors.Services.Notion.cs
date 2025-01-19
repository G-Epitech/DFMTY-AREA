using ErrorOr;

namespace Zeus.Daemon.Domain.Errors.Services;

public static partial class Errors
{
    public static partial class Services
    {
        public static class Notion
        {
            public static Error ErrorDuringSearchRequest => Error.Failure(
                code: "Integrations.Notion.ErrorDuringSearchRequest",
                description: "Error during notion search request."
            );

            public static Error ErrorDuringPostRequest => Error.Failure(
                code: "Integrations.Notion.ErrorDuringPostRequest",
                description: "Error during notion post request."
            );

            public static Error InvalidBody => Error.Validation(
                code: "Integrations.Notion.InvalidBody",
                description: "Invalid body."
            );

            public static Error InvalidLinkRequest => Error.Validation(
                code: "Integrations.Notion.InvalidLinkRequest",
                description: "Invalid link request."
            );
        }
    }
}
