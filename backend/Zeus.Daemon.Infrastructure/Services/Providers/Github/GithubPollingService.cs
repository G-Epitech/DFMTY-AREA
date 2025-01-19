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

    private readonly Dictionary<AccessToken, Dictionary<PullRequestAutomationProps, List<AutomationId>>>
        _registeredPollingPrTokens = new();

    private readonly Dictionary<PullRequestAutomationProps, List<GithubPullRequest>> _cachedPullRequests = new();

    private readonly Dictionary<AutomationId, Func<AutomationId, GithubPullRequest, Task>>
        _registeredNewPrHandlers = new();

    private readonly Dictionary<AutomationId, Func<AutomationId, GithubPullRequest, Task>>
        _registeredRemovePrHandlers = new();

    private readonly Dictionary<AccessToken, Dictionary<IssueAutomationProps, List<AutomationId>>>
        _registeredPollingIssuesTokens = new();

    private readonly Dictionary<IssueAutomationProps, List<GithubIssue>> _cachedIssues = new();

    private readonly Dictionary<AutomationId, Func<AutomationId, GithubIssue, Task>>
        _registeredNewIssuesHandlers = new();

    private readonly Dictionary<AutomationId, Func<AutomationId, GithubIssue, Task>>
        _registeredRemoveIssuesHandlers = new();

    public GithubPollingService(IGithubApiService apiService, ILogger<GithubPollingService> logger)
    {
        _apiService = apiService;
        _logger = logger;

        _timer = new Timer(TimeSpan.FromSeconds(3));
        _timer.Elapsed += (sender, e) => _ = PollPullRequests();
        _timer.Elapsed += (sender, e) => _ = PollIssues();
        _timer.AutoReset = true;
    }

    private async Task PollPullRequests()
    {
        foreach (var prToken in _registeredPollingPrTokens)
        {
            foreach (var endpoint in prToken.Value)
            {
                var loadedPullRequests = await _apiService.GetPullRequestsAsync(prToken.Key, endpoint.Key.Owner,
                    endpoint.Key.Repository);

                if (loadedPullRequests.IsError)
                {
                    _logger.LogWarning("Error while polling PRs ({owner}/{repository}): {error}",
                        endpoint.Key.Owner, endpoint.Key.Repository, loadedPullRequests.Errors);
                    continue;
                }

                var newPullRequests = loadedPullRequests.Value.Except(_cachedPullRequests[endpoint.Key]).ToList();
                var removedPullRequests = _cachedPullRequests[endpoint.Key].Except(loadedPullRequests.Value).ToList();

                foreach (var pr in newPullRequests)
                {
                    foreach (AutomationId automationId in endpoint.Value)
                    {
                        if (_registeredNewPrHandlers.TryGetValue(automationId, out var handler))
                        {
                            _ = handler(automationId, pr);
                        }
                    }
                }

                foreach (var pr in removedPullRequests)
                {
                    foreach (AutomationId automationId in endpoint.Value)
                    {
                        if (_registeredRemovePrHandlers.TryGetValue(automationId, out var handler))
                        {
                            _ = handler(automationId, pr);
                        }
                    }
                }

                _cachedPullRequests[endpoint.Key] = loadedPullRequests.Value;
            }
        }
    }

    private async Task PollIssues()
    {
        foreach (var issueToken in _registeredPollingIssuesTokens)
        {
            foreach (var endpoint in issueToken.Value)
            {
                var loadedIssues = await _apiService.GetIssuesAsync(issueToken.Key, endpoint.Key.Owner,
                    endpoint.Key.Repository);

                if (loadedIssues.IsError)
                {
                    _logger.LogWarning("Error while polling issues ({owner}/{repository}): {error}",
                        endpoint.Key.Owner, endpoint.Key.Repository, loadedIssues.Errors);
                    continue;
                }

                var newIssues = loadedIssues.Value.Except(_cachedIssues[endpoint.Key]).ToList();
                var removedIssues = _cachedIssues[endpoint.Key].Except(loadedIssues.Value).ToList();

                foreach (var issue in newIssues)
                {
                    foreach (AutomationId automationId in endpoint.Value)
                    {
                        if (_registeredNewIssuesHandlers.TryGetValue(automationId, out var handler))
                        {
                            _ = handler(automationId, issue);
                        }
                    }
                }

                foreach (var issue in removedIssues)
                {
                    foreach (AutomationId automationId in endpoint.Value)
                    {
                        if (_registeredRemoveIssuesHandlers.TryGetValue(automationId, out var handler))
                        {
                            _ = handler(automationId, issue);
                        }
                    }
                }

                _cachedIssues[endpoint.Key] = loadedIssues.Value;
            }
        }
    }

    public async Task<bool> RegisterNewPullRequestDetectedAsync(AutomationId automationId, AccessToken accessToken,
        string owner,
        string repository, Func<AutomationId, GithubPullRequest, Task> handler,
        CancellationToken cancellationToken = default)
    {
        if (!_registeredPollingPrTokens.TryGetValue(accessToken,
                out Dictionary<PullRequestAutomationProps, List<AutomationId>>? automations))
        {
            automations = new Dictionary<PullRequestAutomationProps, List<AutomationId>>();
            _registeredPollingPrTokens.Add(accessToken, automations);
        }

        var props = new PullRequestAutomationProps(owner, repository);

        _registeredNewPrHandlers.Add(automationId, handler);

        if (!automations.TryGetValue(props, out var handlers))
        {
            handlers = [];
            automations.Add(props, handlers);
        }

        handlers.Add(automationId);

        if (_cachedPullRequests.ContainsKey(props))
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

        _cachedPullRequests.Add(props, pullRequests.Value);

        return true;
    }

    public async Task<bool> RegisterRemovePullRequestDetectedAsync(AutomationId automationId, AccessToken accessToken,
        string owner,
        string repository, Func<AutomationId, GithubPullRequest, Task> handler,
        CancellationToken cancellationToken = default)
    {
        if (!_registeredPollingPrTokens.TryGetValue(accessToken,
                out Dictionary<PullRequestAutomationProps, List<AutomationId>>? automations))
        {
            automations = new Dictionary<PullRequestAutomationProps, List<AutomationId>>();
            _registeredPollingPrTokens.Add(accessToken, automations);
        }

        var props = new PullRequestAutomationProps(owner, repository);

        _registeredRemovePrHandlers.Add(automationId, handler);

        if (!automations.TryGetValue(props, out var handlers))
        {
            handlers = [];
            automations.Add(props, handlers);
        }

        handlers.Add(automationId);

        if (_cachedPullRequests.ContainsKey(props))
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

        _cachedPullRequests.Add(props, pullRequests.Value);

        return true;
    }

    public async Task<bool> RegisterNewIssueDetectedAsync(AutomationId automationId, AccessToken accessToken,
        string owner,
        string repository,
        Func<AutomationId, GithubIssue, Task> handler, CancellationToken cancellationToken = default)
    {
        if (!_registeredPollingIssuesTokens.TryGetValue(accessToken,
                out Dictionary<IssueAutomationProps, List<AutomationId>>? automations))
        {
            automations = new Dictionary<IssueAutomationProps, List<AutomationId>>();
            _registeredPollingIssuesTokens.Add(accessToken, automations);
        }

        var props = new IssueAutomationProps(owner, repository);

        _registeredNewIssuesHandlers.Add(automationId, handler);

        if (!automations.TryGetValue(props, out var handlers))
        {
            handlers = [];
            automations.Add(props, handlers);
        }

        handlers.Add(automationId);

        if (_cachedIssues.ContainsKey(props))
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

        _cachedIssues.Add(props, issues.Value);

        return true;
    }

    public async Task<bool> RegisterRemoveIssueDetectedAsync(AutomationId automationId, AccessToken accessToken,
        string owner,
        string repository, Func<AutomationId, GithubIssue, Task> handler, CancellationToken cancellationToken = default)
    {
        if (!_registeredPollingIssuesTokens.TryGetValue(accessToken,
                out Dictionary<IssueAutomationProps, List<AutomationId>>? automations))
        {
            automations = new Dictionary<IssueAutomationProps, List<AutomationId>>();
            _registeredPollingIssuesTokens.Add(accessToken, automations);
        }

        var props = new IssueAutomationProps(owner, repository);

        _registeredRemoveIssuesHandlers.Add(automationId, handler);

        if (!automations.TryGetValue(props, out var handlers))
        {
            handlers = [];
            automations.Add(props, handlers);
        }

        handlers.Add(automationId);

        if (_cachedIssues.ContainsKey(props))
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

        _cachedIssues.Add(props, issues.Value);

        return true;
    }

    public void UnregisterNewPullRequestDetected(AutomationId automationId)
    {
        _registeredNewPrHandlers.Remove(automationId);

        foreach ((AccessToken accessToken, Dictionary<PullRequestAutomationProps, List<AutomationId>> automations) in
                 _registeredPollingPrTokens)
        {
            foreach (var pair in automations)
            {
                pair.Value.Remove(automationId);

                if (pair.Value.Count != 0)
                {
                    continue;
                }

                automations.Remove(pair.Key);
                _cachedPullRequests.Remove(pair.Key);
            }

            if (automations.Count != 0)
            {
                continue;
            }

            _registeredPollingPrTokens.Remove(accessToken);
        }
    }

    public void UnregisterRemovePullRequestDetected(AutomationId automationId)
    {
        _registeredRemovePrHandlers.Remove(automationId);

        foreach ((AccessToken accessToken, Dictionary<PullRequestAutomationProps, List<AutomationId>> automations) in
                 _registeredPollingPrTokens)
        {
            foreach (var pair in automations)
            {
                pair.Value.Remove(automationId);

                if (pair.Value.Count != 0)
                {
                    continue;
                }

                automations.Remove(pair.Key);
                _cachedPullRequests.Remove(pair.Key);
            }

            if (automations.Count != 0)
            {
                continue;
            }

            _registeredPollingPrTokens.Remove(accessToken);
        }
    }

    public void UnregisterNewIssueDetected(AutomationId automationId)
    {
        _registeredNewIssuesHandlers.Remove(automationId);

        foreach ((AccessToken accessToken, Dictionary<IssueAutomationProps, List<AutomationId>> automations) in
                 _registeredPollingIssuesTokens)
        {
            foreach (var pair in automations)
            {
                pair.Value.Remove(automationId);

                if (pair.Value.Count != 0)
                {
                    continue;
                }

                automations.Remove(pair.Key);
                _cachedIssues.Remove(pair.Key);
            }

            if (automations.Count != 0)
            {
                continue;
            }

            _registeredPollingIssuesTokens.Remove(accessToken);
        }
    }

    public void UnregisterRemoveIssueDetected(AutomationId automationId)
    {
        _registeredRemoveIssuesHandlers.Remove(automationId);

        foreach ((AccessToken accessToken, Dictionary<IssueAutomationProps, List<AutomationId>> automations) in
                 _registeredPollingIssuesTokens)
        {
            foreach (var pair in automations)
            {
                pair.Value.Remove(automationId);

                if (pair.Value.Count != 0)
                {
                    continue;
                }

                automations.Remove(pair.Key);
                _cachedIssues.Remove(pair.Key);
            }

            if (automations.Count != 0)
            {
                continue;
            }

            _registeredPollingIssuesTokens.Remove(accessToken);
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
