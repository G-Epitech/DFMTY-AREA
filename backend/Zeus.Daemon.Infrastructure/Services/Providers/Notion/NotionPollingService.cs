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
    private readonly Dictionary<AccessToken, List<NotionDatabaseId>> _cachedDatabases = new();

    private readonly Dictionary<AutomationId, Func<AutomationId, NotionDatabase, CancellationToken, Task>>
        _registeredDatabaseHandlers = new();

    private readonly Dictionary<AccessToken, List<AutomationId>> _registeredPollingPagesTokens = new();
    private readonly Dictionary<AccessToken, List<NotionPageId>> _cachedPages = new();

    private readonly Dictionary<AutomationId, Func<AutomationId, NotionPage, CancellationToken, Task>>
        _registeredPageHandlers = new();

    public NotionPollingService(INotionApiService apiService, ILogger<NotionPollingService> logger)
    {
        _apiService = apiService;
        _logger = logger;

        _timer = new Timer(TimeSpan.FromSeconds(10));
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
        _registeredDatabaseHandlers.Add(automationId, handler);

        if (_cachedDatabases.ContainsKey(accessToken))
        {
            return true;
        }

        var databases = await _apiService.GetWorkspaceDatabasesAsync(accessToken, cancellationToken);
        if (databases.IsError)
        {
            return false;
        }

        _cachedDatabases.Add(accessToken, databases.Value.Select(x => x.Id).ToList());

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
        _registeredPageHandlers.Add(automationId, handler);

        if (_cachedPages.ContainsKey(accessToken))
        {
            return true;
        }

        var pages = await _apiService.GetWorkspacePagesAsync(accessToken, cancellationToken);
        if (pages.IsError)
        {
            return false;
        }

        _cachedPages.Add(accessToken, pages.Value.Select(x => x.Id).ToList());

        return true;
    }

    public void UnregisterNewDatabaseDetected(AutomationId automationId)
    {
        _registeredDatabaseHandlers.Remove(automationId);

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
        _registeredPageHandlers.Remove(automationId);

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
            var databases = await _apiService.GetWorkspaceDatabasesAsync(databasesToken.Key);
            if (databases.IsError)
            {
                _logger.LogError("Error polling databases: {error}", databases.Errors);
                continue;
            }

            var newDatabases = databases.Value.Where(x => !_cachedDatabases[databasesToken.Key].Contains(x.Id))
                .ToList();

            foreach (var newDatabase in newDatabases)
            {
                foreach (var automationId in databasesToken.Value)
                {
                    var handler = _registeredDatabaseHandlers[automationId];
                    _ = handler(automationId, newDatabase, CancellationToken.None);
                }
            }

            _cachedDatabases[databasesToken.Key] = databases.Value.Select(x => x.Id).ToList();
        }
    }

    private async Task PollPages()
    {
        foreach (var pagesToken in _registeredPollingPagesTokens)
        {
            var pages = await _apiService.GetWorkspacePagesAsync(pagesToken.Key);
            if (pages.IsError)
            {
                _logger.LogError("Error polling pages: {error}", pages.Errors);
                continue;
            }

            var newPages = pages.Value.Where(x => !_cachedPages[pagesToken.Key].Contains(x.Id)).ToList();

            foreach (var newPage in newPages)
            {
                foreach (var automationId in pagesToken.Value)
                {
                    var handler = _registeredPageHandlers[automationId];
                    _ = handler(automationId, newPage, CancellationToken.None);
                }
            }

            _cachedPages[pagesToken.Key] = pages.Value.Select(x => x.Id).ToList();
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
