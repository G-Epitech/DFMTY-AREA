using Microsoft.Extensions.Logging;

using Zeus.Common.Domain.Authentication.Common;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Daemon.Application.Interfaces;
using Zeus.Daemon.Application.Providers.Github.Services;
using Zeus.Daemon.Domain.Providers.Github;

using Timer = System.Timers.Timer;

namespace Zeus.Daemon.Infrastructure.Services.Providers.Github;

public class GithubPollingService : IGithubPollingService, IDaemonService
{
    private readonly IGithubApiService _apiService;
    private readonly Timer _timer;
    private readonly ILogger _logger;

    private readonly Dictionary<AccessToken, Dictionary<AutomationId, PullRequestAutomationProps>>
        _registeredPollingPrTokens = new();

    private readonly Dictionary<AccessToken, List<GithubPullRequest>> _cachedPullRequests = new();

    private readonly Dictionary<AutomationId, Func<AutomationId, GithubPullRequest, Task>>
        _registeredNewPrHandlers = new();

    private readonly Dictionary<AutomationId, Func<AutomationId, GithubPullRequest, Task>>
        _registeredRemovePrHandlers = new();

    private readonly Dictionary<AccessToken, Dictionary<AutomationId, IssueAutomationProps>>
        _registeredPollingIssuesTokens = new();

    private readonly Dictionary<AccessToken, List<GithubIssue>> _cachedIssues = new();

    private readonly Dictionary<AutomationId, Func<AutomationId, GithubIssue, Task>>
        _registeredNewIssuesHandlers = new();

    private readonly Dictionary<AutomationId, Func<AutomationId, GithubIssue, Task>>
        _registeredRemoveIssuesHandlers = new();

    public GithubPollingService(IGithubApiService apiService, ILogger<GithubPollingService> logger)
    {
        _apiService = apiService;
        _logger = logger;

        _timer = new Timer(TimeSpan.FromSeconds(5));
        // _timer.Elapsed += (sender, e) => _ = PollPullRequests();
        // _timer.Elapsed += (sender, e) => _ = PollIssues();
        _timer.AutoReset = true;
    }

    public async Task<bool> RegisterNewPullRequestDetectedAsync(AutomationId automationId, AccessToken accessToken,
        string owner,
        string repository, Func<AutomationId, GithubPullRequest, Task> handler,
        CancellationToken cancellationToken = default)
    {
        if (!_registeredPollingPrTokens.TryGetValue(accessToken,
                out Dictionary<AutomationId, PullRequestAutomationProps>? automations))
        {
            automations = new Dictionary<AutomationId, PullRequestAutomationProps>();
            _registeredPollingPrTokens.Add(accessToken, automations);
        }

        automations.Add(automationId, new PullRequestAutomationProps(owner, repository));
        _registeredNewPrHandlers.Add(automationId, handler);

        if (_cachedPullRequests.ContainsKey(accessToken))
        {
            return true;
        }

        var pullRequests = await _apiService.GetPullRequestsAsync(accessToken, owner, repository, cancellationToken);
        if (pullRequests.IsError)
        {
            _logger.LogWarning("Error while caching PRs ({owner}/{repository}): {error}", owner, repository,
                pullRequests.Errors);
            return false;
        }

        _cachedPullRequests.Add(accessToken, pullRequests.Value);

        return true;
    }

    public async Task<bool> RegisterRemovePullRequestDetectedAsync(AutomationId automationId, AccessToken accessToken,
        string owner,
        string repository, Func<AutomationId, GithubPullRequest, Task> handler,
        CancellationToken cancellationToken = default)
    {
        if (!_registeredPollingPrTokens.TryGetValue(accessToken,
                out Dictionary<AutomationId, PullRequestAutomationProps>? automations))
        {
            automations = new Dictionary<AutomationId, PullRequestAutomationProps>();
            _registeredPollingPrTokens.Add(accessToken, automations);
        }

        automations.Add(automationId, new PullRequestAutomationProps(owner, repository));
        _registeredRemovePrHandlers.Add(automationId, handler);

        if (_cachedPullRequests.ContainsKey(accessToken))
        {
            return true;
        }

        var pullRequests = await _apiService.GetPullRequestsAsync(accessToken, owner, repository, cancellationToken);
        if (pullRequests.IsError)
        {
            _logger.LogWarning("Error while caching PRs ({owner}/{repository}): {error}", owner, repository,
                pullRequests.Errors);
            return false;
        }

        _cachedPullRequests.Add(accessToken, pullRequests.Value);

        return true;
    }

    public async Task<bool> RegisterNewIssueDetectedAsync(AutomationId automationId, AccessToken accessToken,
        string owner,
        string repository,
        Func<AutomationId, GithubIssue, Task> handler, CancellationToken cancellationToken = default)
    {
        if (!_registeredPollingIssuesTokens.TryGetValue(accessToken,
                out Dictionary<AutomationId, IssueAutomationProps>? automations))
        {
            automations = new Dictionary<AutomationId, IssueAutomationProps>();
            _registeredPollingIssuesTokens.Add(accessToken, automations);
        }

        automations.Add(automationId, new IssueAutomationProps(owner, repository));
        _registeredNewIssuesHandlers.Add(automationId, handler);

        if (_cachedIssues.ContainsKey(accessToken))
        {
            return true;
        }

        var issues = await _apiService.GetIssuesAsync(accessToken, owner, repository, cancellationToken);
        if (issues.IsError)
        {
            _logger.LogWarning("Error while caching issues ({owner}/{repository}): {error}", owner, repository,
                issues.Errors);
            return false;
        }

        _cachedIssues.Add(accessToken, issues.Value);

        return true;
    }

    public async Task<bool> RegisterRemoveIssueDetectedAsync(AutomationId automationId, AccessToken accessToken,
        string owner,
        string repository, Func<AutomationId, GithubIssue, Task> handler, CancellationToken cancellationToken = default)
    {
        if (!_registeredPollingIssuesTokens.TryGetValue(accessToken,
                out Dictionary<AutomationId, IssueAutomationProps>? automations))
        {
            automations = new Dictionary<AutomationId, IssueAutomationProps>();
            _registeredPollingIssuesTokens.Add(accessToken, automations);
        }

        automations.Add(automationId, new IssueAutomationProps(owner, repository));
        _registeredRemoveIssuesHandlers.Add(automationId, handler);

        if (_cachedIssues.ContainsKey(accessToken))
        {
            return true;
        }

        var issues = await _apiService.GetIssuesAsync(accessToken, owner, repository, cancellationToken);
        if (issues.IsError)
        {
            _logger.LogWarning("Error while caching issues ({owner}/{repository}): {error}", owner, repository,
                issues.Errors);
            return false;
        }

        _cachedIssues.Add(accessToken, issues.Value);

        return true;
    }

    public void UnregisterNewPullRequestDetected(AutomationId automationId)
    {
        _registeredNewPrHandlers.Remove(automationId);

        foreach ((AccessToken accessToken, Dictionary<AutomationId, PullRequestAutomationProps> automations) in
                 _registeredPollingPrTokens)
        {
            automations.Remove(automationId);

            if (automations.Count != 0)
            {
                continue;
            }

            _registeredPollingPrTokens.Remove(accessToken);
            _cachedPullRequests.Remove(accessToken);
        }
    }

    public void UnregisterRemovePullRequestDetected(AutomationId automationId)
    {
        _registeredRemovePrHandlers.Remove(automationId);

        foreach ((AccessToken accessToken, Dictionary<AutomationId, PullRequestAutomationProps> automations) in
                 _registeredPollingPrTokens)
        {
            automations.Remove(automationId);

            if (automations.Count != 0)
            {
                continue;
            }

            _registeredPollingPrTokens.Remove(accessToken);
            _cachedPullRequests.Remove(accessToken);
        }
    }

    public void UnregisterNewIssueDetected(AutomationId automationId)
    {
        _registeredNewIssuesHandlers.Remove(automationId);

        foreach ((AccessToken accessToken, Dictionary<AutomationId, IssueAutomationProps> automations) in
                 _registeredPollingIssuesTokens)
        {
            automations.Remove(automationId);

            if (automations.Count != 0)
            {
                continue;
            }

            _registeredPollingIssuesTokens.Remove(accessToken);
            _cachedIssues.Remove(accessToken);
        }
    }

    public void UnregisterRemoveIssueDetected(AutomationId automationId)
    {
        _registeredRemoveIssuesHandlers.Remove(automationId);

        foreach ((AccessToken accessToken, Dictionary<AutomationId, IssueAutomationProps> automations) in
                 _registeredPollingIssuesTokens)
        {
            automations.Remove(automationId);

            if (automations.Count != 0)
            {
                continue;
            }

            _registeredPollingIssuesTokens.Remove(accessToken);
            _cachedIssues.Remove(accessToken);
        }
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer.Start();
        _logger.LogInformation("Github polling service started.");
        return Task.CompletedTask;
    }

    public Task StopAsync()
    {
        _timer.Stop();
        _logger.LogInformation("Github polling service stopped.");
        return Task.CompletedTask;
    }

    private record PullRequestAutomationProps(
        string Owner,
        string Repository);

    private record IssueAutomationProps(
        string Owner,
        string Repository);
}
