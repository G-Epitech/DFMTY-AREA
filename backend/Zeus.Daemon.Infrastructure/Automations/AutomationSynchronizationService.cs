using Zeus.Api.gRPC.SDK.Services;
using Zeus.Daemon.Application.Interfaces;
using Zeus.Daemon.Domain.Automation.AutomationAggregate;
using Zeus.Daemon.Domain.Automation.AutomationAggregate.Entities;
using Zeus.Daemon.Domain.Automation.AutomationAggregate.Enums;
using Zeus.Daemon.Domain.Automation.AutomationAggregate.ValueObjects;
using Zeus.Daemon.Domain.IntegrationAggregate.ValueObjects;
using Zeus.Daemon.Domain.User.ValueObjects;

namespace Zeus.Daemon.Infrastructure.Automations;

public class AutomationSynchronizationService
{
    private readonly SynchronizationGrpcService _synchronizationGrpcService;
    private readonly IAutomationHandlersRegistry _automationHandlersRegistry;
    private DateTime _lastUpdate = DateTime.UnixEpoch;
    private const int RefreshIntervalMilliseconds = 1000;

    public AutomationSynchronizationService(SynchronizationGrpcService synchronizationGrpcService,
        IAutomationHandlersRegistry automationHandlersRegistry)
    {
        _synchronizationGrpcService = synchronizationGrpcService;
        _automationHandlersRegistry = automationHandlersRegistry;
    }

    public async Task ListenUpdatesAsync(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await WaitForChangesAsync(cancellationToken);
            Console.WriteLine("Changes detected");
            await RefreshAutomationsAsync(cancellationToken);
        }
    }

    private async Task WaitForChangesAsync(CancellationToken cancellationToken = default)
    {
        bool hasChanges = false;

        while (!cancellationToken.IsCancellationRequested && !hasChanges)
        {
            hasChanges = await _synchronizationGrpcService.HasChangesAsync(_lastUpdate, cancellationToken);
            await Task.Delay(RefreshIntervalMilliseconds, cancellationToken);
        }
    }

    private Task RefreshAutomationsAsync(CancellationToken cancellationToken = default)
    {
        if (_lastUpdate != DateTime.UnixEpoch)
        {
            return Task.CompletedTask;
        }
        
        _lastUpdate = DateTime.UtcNow;

        var triggerParameters = new List<AutomationTriggerParameter>
        {
            new() { Value = "1316046870178697267", Identifier = "GuildId" }, new() { Value = "1316046972733620244", Identifier = "ChannelId" },
        };
        var trigger = AutomationTrigger.Create("Discord.MessageReceivedInChannel", triggerParameters, new List<IntegrationId>());

        var actionParameters = new List<AutomationActionParameter>
        {
            new AutomationActionParameter { Value = "1316046972733620244", Identifier = "ChannelId", Type = AutomationActionParameterType.Raw },
            new AutomationActionParameter { Value = "C'est la fête mes loulous", Identifier = "Content", Type = AutomationActionParameterType.Raw },
        };
        var actions = new List<AutomationAction> { AutomationAction.Create("Discord.SendMessageToChannel", 0, actionParameters, new List<IntegrationId>()) };

        var automation = Automation.Create("Test", "test description", new UserId(Guid.NewGuid()), trigger,
            actions);
        
        var automations = new List<Automation> { automation };

        return _automationHandlersRegistry.RefreshAutomationsAsync(automations, cancellationToken);
    }
}
