using ErrorOr;

namespace Zeus.Api.Domain.Errors.OAuth2;

public static partial class Errors
{
    public static partial class OAuth2
    {
        public static class Google
        {
            public static Error ErrorDuringTokenRequest => Error.Failure(
                code: "OAuth2.Google.ErrorDuringTokenRequest",
                description: "Error during google token request."
            );
            
            public static Error ErrorDuringUserRequest => Error.Failure(
                code: "OAuth2.Google.ErrorDuringUserRequest",
                description: "Error during google user request."
            );
            
            public static Error InvalidBody => Error.Validation(
                code: "OAuth2.Google.InvalidBody",
                description: "Invalid body."
            );
        }
    }
}
