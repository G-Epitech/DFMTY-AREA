using Microsoft.Extensions.Logging;

using Zeus.Common.Domain.Authentication.Common;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Domain.Integrations.IntegrationAggregate;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.Enums;
using Zeus.Daemon.Application.Attributes;
using Zeus.Daemon.Application.Interfaces;
using Zeus.Daemon.Application.Providers.Gmail.Services;
using Zeus.Daemon.Domain.Automations;
using Zeus.Daemon.Domain.Providers.Gmail;
using Zeus.Daemon.Domain.Providers.Gmail.ValueObjects;

namespace Zeus.Daemon.Application.Providers.Gmail.TriggerHandlers;

[TriggerHandler("Gmail.EmailReceived")]
public class GmailEmailReceivedTriggerHandler
{
    private readonly IGmailPollingService _gmailPollingService;
    private readonly IAutomationsLauncher _automationsLauncher;
    private readonly ILogger _logger;
    private readonly Dictionary<GmailWatchingId, EmailReceivedTriggerData> _watchingIdToAutomationId = new();
    private readonly Dictionary<AutomationId, GmailWatchingId> _automationIdToWatchingId = new();
    private readonly IGmailApiService _gmailApiService;

    public GmailEmailReceivedTriggerHandler(
        IGmailPollingService gmailPollingService,
        IGmailApiService gmailApiService,
        ILogger<GmailEmailReceivedTriggerHandler> logger,
        IAutomationsLauncher automationsLauncher)
    {
        _gmailPollingService = gmailPollingService;
        _gmailApiService = gmailApiService;
        _logger = logger;
        _automationsLauncher = automationsLauncher;
        _gmailPollingService.RegisterOnMessagesReceivedHandler(OnEmailsReceivedAsync);
    }

    [OnTriggerRegister]
    public Task<bool> OnRegisterAsync(
        AutomationId automationId,
        [FromIntegrations] GmailIntegration integration
    )
    {
        var integrationToken = integration.Tokens.First(t => t is { Usage: IntegrationTokenUsage.Access, Type: "Bearer" });

        if (!_gmailPollingService.WatchNewEmailReceived(
                new AccessToken(integrationToken.Value),
                new GmailUserId(integration.ClientId),
                CancellationToken.None,
                out var watchingId
            ))
        {
            throw new Exception("Failed to watch for new email received");
        }

        _watchingIdToAutomationId[watchingId] = new EmailReceivedTriggerData { AutomationId = automationId, Integration = integration };
        _automationIdToWatchingId[automationId] = watchingId;
        return Task.FromResult(true);
    }

    [OnTriggerRemove]
    public Task<bool> OnRemoveAsync(
        AutomationId automationId
    )
    {
        if (!_automationIdToWatchingId.TryGetValue(automationId, out var watchingId))
        {
            return Task.FromResult(true);
        }

        _gmailPollingService.UnwatchEmailReceived(watchingId);
        _watchingIdToAutomationId.Remove(watchingId);
        _automationIdToWatchingId.Remove(automationId);
        return Task.FromResult(true);
    }

    private async Task OnEmailsReceivedAsync(
        GmailWatchingId watchingId,
        List<GmailMessageId> messageIds,
        CancellationToken cancellationToken
    )
    {
        if (!_watchingIdToAutomationId.TryGetValue(watchingId, out var data))
        {
            return;
        }

        var integrationToken = data.Integration.Tokens.First(t => t is { Usage: IntegrationTokenUsage.Access, Type: "Bearer" });
        var userId = new GmailUserId(data.Integration.ClientId);
        var accessToken = new AccessToken(integrationToken.Value);

        await Task.WhenAll(messageIds.Select(id => OnEmailReceivedAsync(data.AutomationId, userId, accessToken, id, cancellationToken)));
    }

    private async Task OnEmailReceivedAsync(
        AutomationId automationId,
        GmailUserId userId,
        AccessToken accessToken,
        GmailMessageId messageId,
        CancellationToken cancellationToken
    )
    {
        var res = await _gmailApiService.GetMessageAsync(accessToken, messageId, userId, cancellationToken);

        if (res.IsError)
        {
            _logger.LogError("Failed to get email {messageId} for automation {automationId}: {error}", messageId, automationId, res.FirstError);
            return;
        }
        var message = res.Value;
        var facts = new FactsDictionary
        {
            { "Author", Fact.Create(message.Author) },
            { "From", Fact.Create(message.From) },
            { "Subject", Fact.Create(message.Subject) },
            { "To", Fact.Create(message.To) },
            { "Body", Fact.Create(message.Body) },
            { "ReceptionTime", Fact.Create(message.ReceivedAt) }
        };

        await _automationsLauncher.LaunchAsync(automationId, facts);
    }


    private struct EmailReceivedTriggerData
    {
        public AutomationId AutomationId { get; init; }
        public GmailIntegration Integration { get; init; }
    }
}
