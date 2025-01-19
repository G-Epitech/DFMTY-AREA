using Microsoft.Extensions.Logging;

using Zeus.Common.Domain.Authentication.Common;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Domain.Integrations.IntegrationAggregate;
using Zeus.Daemon.Application.Attributes;
using Zeus.Daemon.Application.Interfaces;
using Zeus.Daemon.Application.Providers.Github.Services;
using Zeus.Daemon.Domain.Automations;
using Zeus.Daemon.Domain.Providers.Github;

namespace Zeus.Daemon.Application.Providers.Github.TriggerHandlers;

[TriggerHandler("Github.IssueCreated")]
public class GithubIssueCreatedTriggerHandler
{
    private readonly IAutomationsLauncher _automationsLauncher;
    private readonly ILogger _logger;
    private readonly IGithubPollingService _pollingService;

    public GithubIssueCreatedTriggerHandler(IAutomationsLauncher automationsLauncher,
        ILogger<GithubIssueCreatedTriggerHandler> logger,
        IGithubPollingService pollingService)
    {
        _automationsLauncher = automationsLauncher;
        _logger = logger;
        _pollingService = pollingService;
    }

    [OnTriggerRegister]
    public async Task<bool> OnRegisterAsync(
        AutomationId automationId,
        [FromParameters] string owner,
        [FromParameters] string repository,
        [FromIntegrations] GithubIntegration integration,
        CancellationToken cancellationToken = default)
    {
        var bearerToken = integration.Tokens.FirstOrDefault(t => t.Type == "Bearer");
        if (bearerToken is null)
        {
            _logger.LogError("Bearer token not found for Github integration {IntegrationId}", integration.Id.Value);
            return false;
        }

        var accessToken = new AccessToken(bearerToken.Value);
        var success = await _pollingService.RegisterNewIssueDetectedAsync(automationId, accessToken, owner,
            repository, OnIssueCreated, cancellationToken);

        return success;
    }

    [OnTriggerRemove]
    public Task<bool> OnRemoveAsync(AutomationId automationId, CancellationToken cancellationToken = default)
    {
        _pollingService.UnregisterNewIssueDetected(automationId);

        return Task.FromResult(true);
    }

    private Task OnIssueCreated(AutomationId automationId, GithubIssue issue)
    {
        var facts = new FactsDictionary
        {
            { "Url", Fact.Create(issue.Uri.ToString()) },
            { "Title", Fact.Create(issue.Title) },
            { "Number", Fact.Create(issue.Number) },
            { "Body", Fact.Create(issue.Body ?? "No body") },
            { "AuthorName", Fact.Create(issue.AuthorName) }
        };

        return _automationsLauncher.LaunchAsync(automationId, facts);
    }
}
