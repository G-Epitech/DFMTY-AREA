using ErrorOr;

namespace Zeus.Api.Domain.Errors.Automations;

public static partial class Errors
{
    public static partial class Automations
    {
        public static Error NotFound => Error.NotFound(
            code: "Automations.NotFound",
            description: "Automation not found."
        );
    }
}
