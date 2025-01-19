using ErrorOr;

using Zeus.Common.Domain.Authentication.Common;
using Zeus.Daemon.Domain.Providers.Github;

namespace Zeus.Daemon.Application.Providers.Github.Services;

public interface IGithubApiService
{
    public Task<ErrorOr<List<GithubPullRequest>>> GetPullRequestsAsync(AccessToken accessToken, string owner,
        string repository,
        CancellationToken cancellationToken = default);

    public Task<ErrorOr<List<GithubIssue>>> GetIssuesAsync(AccessToken accessToken, string owner, string repository,
        CancellationToken cancellationToken = default);

    public Task<ErrorOr<bool>> CreatePullRequestAsync(AccessToken accessToken, string owner, string repository,
        string title, string body,
        string head, string @base, bool draft, CancellationToken cancellationToken = default);

    public Task<ErrorOr<bool>> ClosePullRequestAsync(AccessToken accessToken, string owner, string repository,
        int pullRequestNumber,
        CancellationToken cancellationToken = default);

    public Task<ErrorOr<bool>> MergePullRequestAsync(AccessToken accessToken, string owner, string repository,
        int pullRequestNumber,
        string commitTitle, string commitMessage, string mergeMethod, CancellationToken cancellationToken = default);
}
