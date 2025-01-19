using Zeus.Common.Domain.Authentication.Common;
using Zeus.Common.Domain.Integrations.IntegrationAggregate;
using Zeus.Daemon.Application.Attributes;
using Zeus.Daemon.Application.Execution;
using Zeus.Daemon.Application.Providers.Github.Services;
using Zeus.Daemon.Domain.Automations;

namespace Zeus.Daemon.Application.Providers.Github.ActionsHandlers;

public class GithubMergePullRequestActionHandler
{
    private readonly IGithubApiService _githubApiService;

    public GithubMergePullRequestActionHandler(IGithubApiService githubApiService)
    {
        _githubApiService = githubApiService;
    }

    [ActionHandler("Github.MergePullRequest")]
    public async Task<ActionResult> RunAsync(
        [FromParameters] string owner,
        [FromParameters] string repository,
        [FromParameters] int number,
        [FromParameters] string commitTitle,
        [FromParameters] string commitMessage,
        [FromParameters] string mergeMethod,
        [FromIntegrations] GithubIntegration githubIntegration,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var bearerToken = githubIntegration.Tokens.FirstOrDefault(t => t.Type == "Bearer");
            if (bearerToken is null)
            {
                return new ActionError
                {
                    Message = $"Bearer token not found for Github integration {githubIntegration.Id.Value}"
                };
            }

            var accessToken = new AccessToken(bearerToken.Value);
            var closeResult = await _githubApiService.MergePullRequestAsync(accessToken, owner, repository, number,
                commitTitle, commitMessage, mergeMethod, cancellationToken);

            if (closeResult.IsError)
            {
                return new ActionError
                {
                    Message = "An error occurred while merging the pull request",
                    Details = closeResult.FirstError.Description
                };
            }

            return new FactsDictionary();
        }
        catch (Exception ex)
        {
            return new ActionError
            {
                Details = ex, InnerException = ex, Message = "An error occurred while deleting the database"
            };
        }
    }
}
