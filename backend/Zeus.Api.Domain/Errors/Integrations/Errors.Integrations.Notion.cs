using ErrorOr;

namespace Zeus.Api.Domain.Errors.Integrations;

public static partial class Errors
{
    public static partial class Integrations
    {
        public static class Notion
        {
            public static Error ErrorDuringTokenRequest => Error.Failure(
                code: "Integrations.Notion.ErrorDuringTokenRequest",
                description: "Error during notion token request."
            );

            public static Error ErrorDuringBotRequest => Error.Failure(
                code: "Integrations.Notion.ErrorDuringBotRequest",
                description: "Error during notion bot get request."
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
