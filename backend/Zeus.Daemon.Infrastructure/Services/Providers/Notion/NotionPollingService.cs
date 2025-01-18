using Microsoft.Extensions.Logging;

using Zeus.Common.Domain.Authentication.Common;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Daemon.Application.Interfaces;
using Zeus.Daemon.Application.Providers.Notion.Services;
using Zeus.Daemon.Domain.Providers.Notion;
using Zeus.Daemon.Domain.Providers.Notion.ValueObjects;

using Timer = System.Timers.Timer;

namespace Zeus.Daemon.Infrastructure.Services.Providers.Notion;

public class NotionPollingService : INotionPollingService, IDaemonService
{
    private readonly INotionApiService _apiService;
    private readonly Timer _timer;
    private readonly ILogger _logger;
    private readonly Dictionary<AccessToken, List<AutomationId>> _registeredPollingDatabasesTokens = new();
    private readonly Dictionary<AccessToken, List<NotionDatabase>> _cachedDatabases = new();

    private readonly Dictionary<AutomationId, Func<AutomationId, NotionDatabase, CancellationToken, Task>>
        _registeredNewDatabaseHandlers = new();

    private readonly Dictionary<AccessToken, List<AutomationId>> _registeredPollingPagesTokens = new();
    private readonly Dictionary<AccessToken, List<NotionPage>> _cachedPages = new();

    private readonly Dictionary<AutomationId, Func<AutomationId, NotionPage, CancellationToken, Task>>
        _registeredNewPageHandlers = new();

    private readonly Dictionary<AutomationId, Func<AutomationId, NotionPage, CancellationToken, Task>>
        _registeredRemovePageHandlers = new();

    public NotionPollingService(INotionApiService apiService, ILogger<NotionPollingService> logger)
    {
        _apiService = apiService;
        _logger = logger;

        _timer = new Timer(TimeSpan.FromSeconds(5));
        _timer.Elapsed += (sender, e) => _ = PollDatabases();
        _timer.Elapsed += (sender, e) => _ = PollPages();
        _timer.AutoReset = true;
    }

    public async Task<bool> RegisterNewDatabaseDetected(AutomationId automationId, AccessToken accessToken,
        Func<AutomationId, NotionDatabase, CancellationToken, Task> handler,
        CancellationToken cancellationToken = default)
    {
        if (!_registeredPollingDatabasesTokens.TryGetValue(accessToken, out List<AutomationId>? automations))
        {
            automations = ( []);
            _registeredPollingDatabasesTokens.Add(accessToken, automations);
        }

        automations.Add(automationId);
        _registeredNewDatabaseHandlers.Add(automationId, handler);

        if (_cachedDatabases.ContainsKey(accessToken))
        {
            return true;
        }

        var databases = await _apiService.GetWorkspaceDatabasesAsync(accessToken, cancellationToken);
        if (databases.IsError)
        {
            return false;
        }

        _cachedDatabases.Add(accessToken, databases.Value);

        return true;
    }

    public async Task<bool> RegisterNewPageDetected(AutomationId automationId, AccessToken accessToken,
        Func<AutomationId, NotionPage, CancellationToken, Task> handler, CancellationToken cancellationToken = default)
    {
        if (!_registeredPollingPagesTokens.TryGetValue(accessToken, out List<AutomationId>? automations))
        {
            automations = ( []);
            _registeredPollingPagesTokens.Add(accessToken, automations);
        }

        automations.Add(automationId);
        _registeredNewPageHandlers.Add(automationId, handler);

        if (_cachedPages.ContainsKey(accessToken))
        {
            return true;
        }

        var pages = await _apiService.GetWorkspacePagesAsync(accessToken, cancellationToken);
        if (pages.IsError)
        {
            return false;
        }

        _cachedPages.Add(accessToken, pages.Value);

        return true;
    }

    public async Task<bool> RegisterRemovePageDetected(AutomationId automationId, AccessToken accessToken,
        Func<AutomationId, NotionPage, CancellationToken, Task> handler,
        CancellationToken cancellationToken)
    {
        if (!_registeredPollingPagesTokens.TryGetValue(accessToken, out List<AutomationId>? automations))
        {
            automations = ( []);
            _registeredPollingPagesTokens.Add(accessToken, automations);
        }

        automations.Add(automationId);
        _registeredRemovePageHandlers.Add(automationId, handler);

        if (_cachedPages.ContainsKey(accessToken))
        {
            return true;
        }

        var pages = await _apiService.GetWorkspacePagesAsync(accessToken, cancellationToken);
        if (pages.IsError)
        {
            return false;
        }

        _cachedPages.Add(accessToken, pages.Value);

        return true;
    }

    public void UnregisterNewDatabaseDetected(AutomationId automationId)
    {
        _registeredNewDatabaseHandlers.Remove(automationId);

        foreach (var pollingDatabasesToken in _registeredPollingDatabasesTokens)
        {
            pollingDatabasesToken.Value.Remove(automationId);

            if (pollingDatabasesToken.Value.Count != 0)
            {
                continue;
            }

            _cachedDatabases.Remove(pollingDatabasesToken.Key);
            _registeredPollingDatabasesTokens.Remove(pollingDatabasesToken.Key);
        }
    }

    public void UnregisterNewPageDetected(AutomationId automationId)
    {
        _registeredNewPageHandlers.Remove(automationId);

        foreach (var pollingPagesToken in _registeredPollingPagesTokens)
        {
            pollingPagesToken.Value.Remove(automationId);

            if (pollingPagesToken.Value.Count != 0)
            {
                continue;
            }

            _cachedDatabases.Remove(pollingPagesToken.Key);
            _registeredPollingDatabasesTokens.Remove(pollingPagesToken.Key);
        }
    }

    public void UnregisterRemovePageDetected(AutomationId automationId)
    {
        _registeredRemovePageHandlers.Remove(automationId);

        foreach (var pollingPagesToken in _registeredPollingPagesTokens)
        {
            pollingPagesToken.Value.Remove(automationId);

            if (pollingPagesToken.Value.Count != 0)
            {
                continue;
            }

            _cachedDatabases.Remove(pollingPagesToken.Key);
            _registeredPollingDatabasesTokens.Remove(pollingPagesToken.Key);
        }
    }

    private async Task PollDatabases()
    {
        foreach (var databasesToken in _registeredPollingDatabasesTokens)
        {
            var loadedDatabases = await _apiService.GetWorkspaceDatabasesAsync(databasesToken.Key);
            if (loadedDatabases.IsError)
            {
                _logger.LogError("Error polling databases: {error}", loadedDatabases.Errors);
                continue;
            }

            var newDatabases = loadedDatabases.Value.Where(x => !_cachedDatabases[databasesToken.Key].Contains(x))
                .ToList();

            foreach (var newDatabase in newDatabases)
            {
                foreach (var automationId in databasesToken.Value)
                {
                    var handler = _registeredNewDatabaseHandlers[automationId];

                    _ = handler(automationId, newDatabase, CancellationToken.None);
                }
            }

            _cachedDatabases[databasesToken.Key] = loadedDatabases.Value;
        }
    }

    private async Task PollPages()
    {
        foreach (var pagesToken in _registeredPollingPagesTokens)
        {
            var loadedPages = await _apiService.GetWorkspacePagesAsync(pagesToken.Key);
            if (loadedPages.IsError)
            {
                _logger.LogError("Error polling pages: {error}", loadedPages.Errors);
                continue;
            }

            var newPages = loadedPages.Value.Where(x => !_cachedPages[pagesToken.Key].Contains(x)).ToList();
            foreach (var newPage in newPages)
            {
                foreach (var automationId in pagesToken.Value)
                {
                    if (_registeredNewPageHandlers.TryGetValue(automationId, out var handler))
                    {
                        _ = handler(automationId, newPage, CancellationToken.None);
                    }
                }
            }

            var removedPages = _cachedPages[pagesToken.Key].Where(x => !loadedPages.Value.Contains(x)).ToList();
            foreach (var removedPage in removedPages)
            {
                foreach (var automationId in pagesToken.Value)
                {
                    if (_registeredRemovePageHandlers.TryGetValue(automationId, out var handler))
                    {
                        _ = handler(automationId, removedPage, CancellationToken.None);
                    }
                }
            }

            _cachedPages[pagesToken.Key] = loadedPages.Value;
        }
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer.Start();

        return Task.CompletedTask;
    }

    public Task StopAsync()
    {
        _timer.Stop();

        return Task.CompletedTask;
    }
}
