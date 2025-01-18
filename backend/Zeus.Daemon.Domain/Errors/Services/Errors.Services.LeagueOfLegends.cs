using ErrorOr;

namespace Zeus.Daemon.Domain.Errors.Services;

public static partial class Errors
{
    public static partial class Services
    {
        public static class LeagueOfLegends
        {
            public static Error FailureDuringRequest => Error.Failure(
                code: "Integrations.LeagueOfLegends.FailureDuringRequest",
                description: "Failure during request."
            );

            public static Error InvalidBody => Error.Validation(
                code: "Integrations.LeagueOfLegends.InvalidBody",
                description: "Invalid body."
            );

            public static Error MatchNotFound => Error.NotFound(
                code: "Integrations.LeagueOfLegends.MatchNotFound",
                description: "Match not found."
            );
        }
    }
}
