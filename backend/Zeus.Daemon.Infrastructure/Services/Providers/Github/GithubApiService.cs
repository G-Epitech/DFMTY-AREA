using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;

using ErrorOr;

using Zeus.Common.Domain.Authentication.Common;
using Zeus.Daemon.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Daemon.Application.Providers.Github.Services;
using Zeus.Daemon.Domain.Errors.Services;
using Zeus.Daemon.Domain.Providers.Github;
using Zeus.Daemon.Domain.Providers.Github.ValueObjects;
using Zeus.Daemon.Infrastructure.Services.Providers.Github.Contracts;

namespace Zeus.Daemon.Infrastructure.Services.Providers.Github;

public class GithubApiService : IGithubApiService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public GithubApiService(IIntegrationsSettingsProvider settingsProvider)
    {
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };

        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(settingsProvider.Github.ApiEndpoint);

        _httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/vnd.github.raw+json"));
        _httpClient.DefaultRequestHeaders.UserAgent.Add(
            new ProductInfoHeaderValue("Zeus", "1.0"));
    }

    public async Task<ErrorOr<List<GithubPullRequest>>> GetPullRequestsAsync(AccessToken accessToken, string owner,
        string repository,
        CancellationToken cancellationToken = default)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Value);
        
        HttpResponseMessage response = await _httpClient.GetAsync(
            $"repos/{owner}/{repository}/pulls",
            cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return Errors.Services.Github.ErrorDuringPullRequestFetch;
        }

        var responseContent =
            await response.Content.ReadFromJsonAsync<List<GetGithubPullRequestResult>>(_jsonSerializerOptions,
                cancellationToken);
        if (responseContent == null)
        {
            return Errors.Services.Github.InvalidBody;
        }

        return responseContent.Select(pr =>
                new GithubPullRequest(new GithubPullRequestId(pr.Id), new Uri(pr.HtmlUrl), pr.Title, pr.Number, pr.Body,
                    pr.User.Login))
            .ToList();
    }

    public async Task<ErrorOr<List<GithubIssue>>> GetIssuesAsync(AccessToken accessToken, string owner,
        string repository,
        CancellationToken cancellationToken = default)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Value);
        
        HttpResponseMessage response = await _httpClient.GetAsync(
            $"repos/{owner}/{repository}/issues",
            cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return Errors.Services.Github.ErrorDuringIssueFetch;
        }

        var responseContent =
            await response.Content.ReadFromJsonAsync<List<GetGithubIssueResult>>(_jsonSerializerOptions,
                cancellationToken);
        if (responseContent == null)
        {
            return Errors.Services.Github.InvalidBody;
        }

        return responseContent.Select(issue =>
                new GithubIssue(new GithubIssueId(issue.Id), new Uri(issue.HtmlUrl), issue.Title, issue.Number,
                    issue.Body,
                    issue.User.Login))
            .ToList();
    }

    public async Task<ErrorOr<bool>> CreatePullRequestAsync(AccessToken accessToken, string owner, string repository,
        string title, string body,
        string head, string @base, bool draft, CancellationToken cancellationToken = default)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Value);
        
        var requestBody = new JsonObject
        {
            ["title"] = title,
            ["head"] = head,
            ["base"] = @base,
            ["body"] = body,
            ["draft"] = draft
        };

        HttpResponseMessage response = await _httpClient.PostAsJsonAsync(
            $"repos/{owner}/{repository}/pulls",
            requestBody,
            cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return Errors.Services.Github.ErrorDuringPullRequestCreation;
        }

        return true;
    }

    public async Task<ErrorOr<bool>> ClosePullRequestAsync(AccessToken accessToken, string owner, string repository,
        int pullRequestNumber,
        CancellationToken cancellationToken = default)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Value);
        
        var requestBody = new JsonObject { ["state"] = "closed" };

        HttpResponseMessage response = await _httpClient.PatchAsJsonAsync(
            $"repos/{owner}/{repository}/pulls/{pullRequestNumber}",
            requestBody,
            cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return Errors.Services.Github.ErrorDuringPullRequestClose;
        }

        return true;
    }

    public async Task<ErrorOr<bool>> MergePullRequestAsync(AccessToken accessToken, string owner, string repository,
        int pullRequestNumber,
        string commitTitle, string commitMessage, string mergeMethod, CancellationToken cancellationToken = default)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Value);
        
        var requestBody = new JsonObject
        {
            ["commit_title"] = commitTitle, ["commit_message"] = commitMessage, ["merge_method"] = mergeMethod
        };

        HttpResponseMessage response = await _httpClient.PutAsJsonAsync(
            $"repos/{owner}/{repository}/pulls/{pullRequestNumber}/merge",
            requestBody,
            cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return Errors.Services.Github.ErrorDuringPullRequestMerge;
        }

        return true;
    }
}
