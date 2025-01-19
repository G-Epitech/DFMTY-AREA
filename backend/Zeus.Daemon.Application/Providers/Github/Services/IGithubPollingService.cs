using Zeus.Common.Domain.Authentication.Common;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Daemon.Domain.Providers.Github;

namespace Zeus.Daemon.Application.Providers.Github.Services;

public interface IGithubPollingService
{
    /// <summary>
    /// Register a new pull request detected event.
    /// </summary>
    /// <param name="automationId">Automation id</param>
    /// <param name="accessToken">Automation github access token</param>
    /// <param name="owner">The owner of the repo</param>
    /// <param name="repository">The repos to watch</param>
    /// <param name="handler">Handler when new pull request detected</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> RegisterNewPullRequestDetectedAsync(AutomationId automationId, AccessToken accessToken,
        string owner, string repository, Func<AutomationId, GithubPullRequest, Task> handler,
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Register a remove pull request detected event.
    /// </summary>
    /// <param name="automationId">Automation id</param>
    /// <param name="accessToken">Automation github access token</param>
    /// <param name="owner">The owner of the repo</param>
    /// <param name="repository">The repos to watch</param>
    /// <param name="handler">Handler when remove pull request detected</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> RegisterRemovePullRequestDetectedAsync(AutomationId automationId, AccessToken accessToken,
        string owner, string repository, Func<AutomationId, GithubPullRequest, Task> handler,
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Register a new issue detected event.
    /// </summary>
    /// <param name="automationId">Automation id</param>
    /// <param name="accessToken">Automation github access token</param>
    /// <param name="owner">The owner of the repo</param>
    /// <param name="repository">The repos to watch</param>
    /// <param name="handler">Handler when new issue detected</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> RegisterNewIssueDetectedAsync(AutomationId automationId, AccessToken accessToken,
        string owner, string repository, Func<AutomationId, GithubIssue, Task> handler,
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Register a remove issue detected event.
    /// </summary>
    /// <param name="automationId">Automation id</param>
    /// <param name="accessToken">Automation github access token</param>
    /// <param name="owner">The owner of the repo</param>
    /// <param name="repository">The repos to watch</param>
    /// <param name="handler">Handler when remove issue detected</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> RegisterRemoveIssueDetectedAsync(AutomationId automationId, AccessToken accessToken,
        string owner, string repository, Func<AutomationId, GithubIssue, Task> handler,
        CancellationToken cancellationToken = default);
    
    public void UnregisterNewPullRequestDetected(AutomationId automationId);
    
    public void UnregisterRemovePullRequestDetected(AutomationId automationId);
    
    public void UnregisterNewIssueDetected(AutomationId automationId);
    
    public void UnregisterRemoveIssueDetected(AutomationId automationId);
}
