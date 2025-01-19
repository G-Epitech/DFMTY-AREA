using Zeus.Common.Domain.Authentication.Common;
using Zeus.Common.Domain.Integrations.IntegrationAggregate;
using Zeus.Daemon.Application.Attributes;
using Zeus.Daemon.Application.Execution;
using Zeus.Daemon.Application.Providers.Github.Services;
using Zeus.Daemon.Domain.Automations;

namespace Zeus.Daemon.Application.Providers.Github.ActionsHandlers;

public class GithubCreatePullRequestActionHandler
{
    private readonly IGithubApiService _githubApiService;

    public GithubCreatePullRequestActionHandler(IGithubApiService githubApiService)
    {
        _githubApiService = githubApiService;
    }

    [ActionHandler("Github.CreatePullRequest")]
    public async Task<ActionResult> RunAsync(
        [FromParameters] string owner,
        [FromParameters] string repository,
        [FromParameters] string title,
        [FromParameters] string head,
        [FromParameters] string @base,
        [FromParameters] string body,
        [FromParameters] bool draft,
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
            var pullRequest = await _githubApiService.CreatePullRequestAsync(accessToken, owner, repository, title,
                body, head, @base, draft, cancellationToken);
            if (pullRequest.IsError)
            {
                return new ActionError
                {
                    Message = "An error occurred while creating the pull request",
                    Details = pullRequest.FirstError.Description
                };
            }

            return new FactsDictionary
            {
                { "Url", Fact.Create(pullRequest.Value.Uri.ToString()) },
                { "Number", Fact.Create(pullRequest.Value.Number) },
                { "Title", Fact.Create(pullRequest.Value.Title) },
                { "Body", Fact.Create(pullRequest.Value.Body ?? "No body") },
                { "AuthorName", Fact.Create(pullRequest.Value.AuthorName) }
            };
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
