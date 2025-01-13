using ErrorOr;

namespace Zeus.Api.Domain.Errors.Integrations;

public static partial class Errors
{
    public static partial class Integrations
    {
        public static class OpenAi
        {
            public static Error ErrorDuringModelsRequest => Error.Failure(
                code: "Integrations.OpenAi.ErrorDuringModelsRequest",
                description: "Error during openai models request."
            );
            
            public static Error ErrorDuringUsersRequest => Error.Failure(
                code: "Integrations.OpenAi.ErrorDuringUsersRequest",
                description: "Error during openai users request."
            );
            
            public static Error InvalidBody => Error.Validation(
                code: "Integrations.OpenAi.InvalidBody",
                description: "Invalid body."
            );
            
            public static Error OwnerNotFound => Error.NotFound(
                code: "Integrations.OpenAi.OwnerNotFound",
                description: "OpenAi organization Owner not found."
            );
        }
    }
}
