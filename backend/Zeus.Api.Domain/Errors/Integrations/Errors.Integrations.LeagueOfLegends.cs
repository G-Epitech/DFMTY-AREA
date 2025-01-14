using ErrorOr;

namespace Zeus.Api.Domain.Errors.Integrations;

public static partial class Errors
{
    public static partial class Integrations
    {
        public static class LeagueOfLegends
        {
            public static Error ErrorDuringAccountRequest => Error.Failure(
                code: "Integrations.LeagueOfLegends.ErrorDuringAccountRequest",
                description: "Error during LOL account request."
            );

            public static Error ErrorDuringSummonerRequest => Error.Failure(
                code: "Integrations.LeagueOfLegends.ErrorDuringSummonerRequest",
                description: "Error during LOL summoner request."
            );

            public static Error InvalidBody => Error.Validation(
                code: "Integrations.LeagueOfLegends.InvalidBody",
                description: "Invalid body."
            );
        }
    }
}
