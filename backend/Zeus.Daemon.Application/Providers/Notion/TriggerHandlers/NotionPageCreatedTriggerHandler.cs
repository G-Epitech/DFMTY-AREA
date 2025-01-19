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

[TriggerHandler("Notion.PageCreated")]
public class NotionPageCreatedTriggerHandler
{
    private readonly IAutomationsLauncher _automationsLauncher;
    private readonly ILogger _logger;
    private readonly INotionPollingService _pollingService;

    public NotionPageCreatedTriggerHandler(IAutomationsLauncher automationsLauncher,
        ILogger<NotionPageCreatedTriggerHandler> logger,
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
            var success = await _pollingService.RegisterNewPageDetected(automationId, accessToken,
                OnPageCreated,
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
        _pollingService.UnregisterNewPageDetected(automationId);

        return Task.FromResult(true);
    }

    private Task OnPageCreated(AutomationId automationId, NotionPage notionPage,
        CancellationToken cancellationToken)
    {
        var parentId = notionPage.Parent switch
        {
            NotionParentDatabase p => p.Id.Value,
            NotionParentPage p => p.Id.Value,
            NotionParentWorkspace _ => "Workspace",
            _ => "Unknown"
        };

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
