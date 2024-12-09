using ErrorOr;

namespace Zeus.Api.Domain.Errors.Integrations;

public static partial class Errors
{
    public static partial class Integrations
    {
        public static Error NotFound => Error.NotFound(
            code: "Integrations.NotFound",
            description: "Integration not found."
        );
        
        public static Error PropertiesHandlerNotFound => Error.NotFound(
            code: "Integrations.PropertiesHandlerNotFound",
            description: "Properties handler not found."
        );
    }
}
