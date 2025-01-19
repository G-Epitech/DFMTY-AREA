using ErrorOr;

namespace Zeus.Daemon.Domain.Errors.Services;

public static partial class Errors
{
    public static partial class Services
    {
        public static class Github
        {
            public static Error ErrorDuringPullRequestFetch => Error.Failure(
                code: "Integrations.Github.ErrorDuringPullRequestFetch",
                description: "Error during github pull request fetch."
            );
            
            public static Error ErrorDuringPullRequestCreation => Error.Failure(
                code: "Integrations.Github.ErrorDuringPullRequestCreation",
                description: "Error during github pull request creation."
            );
            
            public static Error ErrorDuringPullRequestClose => Error.Failure(
                code: "Integrations.Github.ErrorDuringPullRequestClose",
                description: "Error during github pull request close."
            );
            
            public static Error ErrorDuringPullRequestMerge => Error.Failure(
                code: "Integrations.Github.ErrorDuringPullRequestMerge",
                description: "Error during github pull request merge."
            );
            
            public static Error ErrorDuringIssueFetch => Error.Failure(
                code: "Integrations.Github.ErrorDuringIssueFetch",
                description: "Error during github issue fetch."
            );

            public static Error InvalidBody => Error.Validation(
                code: "Integrations.Github.InvalidBody",
                description: "Invalid body."
            );

            public static Error InvalidLinkRequest => Error.Validation(
                code: "Integrations.Github.InvalidLinkRequest",
                description: "Invalid link request."
            );
        }
    }
}
