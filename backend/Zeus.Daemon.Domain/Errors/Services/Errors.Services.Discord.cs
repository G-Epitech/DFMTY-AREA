using ErrorOr;

namespace Zeus.Daemon.Domain.Errors.Services;

public static partial class Errors
{
    public static partial class Services
    {
        public static class Discord
        {
            public static Error FailureDuringRequest => Error.Failure(
                code: "Integrations.Discord.FailureDuringRequest",
                description: "Failure during request."
            );
        }
    }
}
