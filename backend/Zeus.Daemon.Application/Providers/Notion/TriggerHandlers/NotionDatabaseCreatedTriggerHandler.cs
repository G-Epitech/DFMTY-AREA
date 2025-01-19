using Microsoft.Extensions.Logging;

using Zeus.Common.Domain.Authentication.Common;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Domain.Integrations.IntegrationAggregate;
using Zeus.Daemon.Application.Attributes;
using Zeus.Daemon.Application.Interfaces;
using Zeus.Daemon.Application.Providers.Notion.Services;
using Zeus.Daemon.Domain.Automations;
using Zeus.Daemon.Domain.Providers.Notion;
using Zeus.Daemon.Domain.Providers.Notion.ValueObjects;

namespace Zeus.Daemon.Application.Providers.Notion.TriggerHandlers;

[TriggerHandler("Notion.DatabaseCreated")]
public class NotionDatabaseCreatedTriggerHandler
{
    private readonly IAutomationsLauncher _automationsLauncher;
    private readonly ILogger _logger;
    private readonly INotionPollingService _pollingService;

    public NotionDatabaseCreatedTriggerHandler(IAutomationsLauncher automationsLauncher,
        ILogger<NotionDatabaseCreatedTriggerHandler> logger,
        INotionPollingService pollingService)
    {
        _automationsLauncher = automationsLauncher;
        _logger = logger;
        _pollingService = pollingService;
    }

    [OnTriggerRegister]
    public async Task<bool> OnRegisterAsync(
        AutomationId automationId,
        [FromIntegrations] IList<NotionIntegration> integrations,
        CancellationToken cancellationToken = default)
    {
        foreach (var integration in integrations)
        {
            var bearerToken = integration.Tokens.FirstOrDefault(t => t.Type == "Bearer");
            if (bearerToken is null)
            {
                return false;
            }

            var accessToken = new AccessToken(bearerToken.Value);
            var success = await _pollingService.RegisterNewDatabaseDetected(automationId, accessToken,
                OnDatabaseCreated,
                cancellationToken);

            if (!success)
            {
                return false;
            }
        }

        return true;
    }

    [OnTriggerRemove]
    public Task<bool> OnRemoveAsync(AutomationId automationId, CancellationToken cancellationToken = default)
    {
        _pollingService.UnregisterNewDatabaseDetected(automationId);

        return Task.FromResult(true);
    }

    private Task OnDatabaseCreated(AutomationId automationId, NotionDatabase notionDatabase,
        CancellationToken cancellationToken)
    {
        var parentId = notionDatabase.Parent switch
        {
            NotionParentDatabase p => p.Id.Value,
            NotionParentPage p => p.Id.Value,
            NotionParentWorkspace _ => "Workspace",
            _ => "Unknown"
        };

        var facts = new FactsDictionary
        {
            { "Id", Fact.Create(notionDatabase.Id.Value) },
            { "Title", Fact.Create(notionDatabase.Title) },
            { "Description", Fact.Create(notionDatabase.Description ?? "No description") },
            { "Icon", Fact.Create(notionDatabase.Icon ?? "No icon") },
            { "ParentId", Fact.Create(parentId) }
        };

        return _automationsLauncher.LaunchAsync(automationId, facts);
    }
}
