using ErrorOr;

namespace Zeus.Api.Domain.Errors.Integrations;

public static partial class Errors
{
    public static partial class Integrations
    {
        public static class Discord
        {
            public static Error InvalidMethod => Error.Failure(
                code: "Integrations.Discord.InvalidMethod",
                description: "Invalid method."
            );
            
            public static Error InvalidBody => Error.Validation(
                code: "Integrations.Discord.InvalidBody",
                description: "Invalid body."
            );
            
            public static Error InvalidLinkRequest => Error.Validation(
                code: "Integrations.Discord.InvalidLinkRequest",
                description: "Invalid link request."
            );
            
            public static Error InvalidDiscordUserCredentials => Error.Unauthorized(
                code: "Integrations.Discord.InvalidDiscordUserCredentials",
                description: "Invalid discord user credentials."
            );
        }
    }
}
