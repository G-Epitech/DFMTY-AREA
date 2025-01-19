using ErrorOr;

namespace Zeus.Daemon.Domain.Errors.Services;

public static partial class Errors
{
    public static partial class Services
    {
        public static class Gmail
        {
            public static Error FailureDuringRequest => Error.Failure(
                code: "Integrations.Gmail.FailureDuringRequest",
                description: "Failure during request."
            );

            public static Error InvalidBody => Error.Failure(
                code: "Integrations.Gmail.InvalidBody",
                description: "Invalid body."
            );
        }
    }
}
