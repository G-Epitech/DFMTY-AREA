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

[TriggerHandler("Notion.DatabaseRowCreated")]
public class NotionDatabaseRowCreatedTriggerHandler
{
    private readonly IAutomationsLauncher _automationsLauncher;
    private readonly ILogger _logger;
    private readonly INotionPollingService _pollingService;
    private readonly Dictionary<AutomationId, NotionDatabaseId> _parentIds = new();

    public NotionDatabaseRowCreatedTriggerHandler(IAutomationsLauncher automationsLauncher,
        ILogger<NotionDatabaseRowCreatedTriggerHandler> logger,
        INotionPollingService pollingService)
    {
        _automationsLauncher = automationsLauncher;
        _logger = logger;
        _pollingService = pollingService;
    }

    [OnTriggerRegister]
    public async Task<bool> OnRegisterAsync(
        AutomationId automationId,
        [FromParameters] string databaseId,
        [FromIntegrations] NotionIntegration integration,
        CancellationToken cancellationToken = default)
    {
        var bearerToken = integration.Tokens.FirstOrDefault(t => t.Type == "Bearer");
        if (bearerToken is null)
        {
            return false;
        }

        var accessToken = new AccessToken(bearerToken.Value);
        var success = await _pollingService.RegisterNewPageDetected(automationId, accessToken,
            OnPageCreated,
            cancellationToken);

        if (!success)
        {
            return false;
        }

        _parentIds[automationId] = new NotionDatabaseId(databaseId);

        return true;
    }

    [OnTriggerRemove]
    public Task<bool> OnRemoveAsync(AutomationId automationId, CancellationToken cancellationToken = default)
    {
        _pollingService.UnregisterNewPageDetected(automationId);
        _parentIds.Remove(automationId);

        return Task.FromResult(true);
    }

    private Task OnPageCreated(AutomationId automationId, NotionPage notionPage,
        CancellationToken cancellationToken)
    {
        if (notionPage.Parent.Type != NotionParentType.Database)
        {
            return Task.CompletedTask;
        }

        var parent = (NotionParentDatabase)notionPage.Parent;
        if (!_parentIds.TryGetValue(automationId, out var parentId) || parent.Id != parentId)
        {
            return Task.CompletedTask;
        }

        var facts = new FactsDictionary
        {
            { "Id", Fact.Create(notionPage.Id.Value) },
            { "Title", Fact.Create(notionPage.Title) },
            { "Description", Fact.Create(notionPage.Description ?? "No description") },
            { "Icon", Fact.Create(notionPage.Icon ?? "No icon") },
            { "ParentId", Fact.Create(parentId) }
        };

        return _automationsLauncher.LaunchAsync(automationId, facts);
    }
}
