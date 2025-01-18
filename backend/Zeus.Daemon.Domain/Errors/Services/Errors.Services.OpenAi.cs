using ErrorOr;

namespace Zeus.Daemon.Domain.Errors.Services;

public static partial class Errors
{
    public static partial class Services
    {
        public static class OpenAi
        {
            public static Error FailureDuringRequest => Error.Failure(
                code: "Integrations.OpenAi.FailureDuringRequest",
                description: "Failure during request."
            );

            public static Error InvalidBody => Error.Validation(
                code: "Integrations.OpenAi.InvalidBody",
                description: "Invalid body."
            );
        }
    }
}
