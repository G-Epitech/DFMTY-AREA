using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Domain.Integrations.IntegrationAggregate;
using Zeus.Daemon.Application.Attributes;
using Zeus.Daemon.Application.Interfaces;
using Zeus.Daemon.Domain.Automations;

namespace Zeus.Daemon.Application.Discord.TriggerHandlers;

[TriggerHandler("Discord.MessageReceivedInChannel")]
public class DiscordMessageReceivedTriggerHandler
{
    private readonly IAutomationsLauncher _automationsLauncher;

    public DiscordMessageReceivedTriggerHandler(IAutomationsLauncher automationsLauncher)
    {
        _automationsLauncher = automationsLauncher;
    }

    [OnTriggerRegister]
    public Task<bool> OnRegisterAsync(
        AutomationId automationId,
        [FromParameters] string channelId,
        [FromParameters] string guildId,
        [FromIntegrations] DiscordIntegration integration,
        CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"DiscordMessageReceivedTriggerHandler.OnRegisterAsync with channelId: {channelId} and guildId: {guildId}");
        Task.Run(async () => await RunAfter2S(automationId, cancellationToken), cancellationToken);
        return Task.FromResult(true);
    }

    [OnTriggerRemove]
    public Task<bool> OnRemoveAsync(AutomationId automationId, CancellationToken cancellationToken = default)
    {
        Console.WriteLine("DiscordMessageReceivedTriggerHandler.OnRemoveAsync");
        return Task.FromResult(true);
    }

    private async Task RunAfter2S(AutomationId id, CancellationToken cancellationToken)
    {
        Console.WriteLine("DiscordMessageReceivedTriggerHandler.RunAfter2s");
        await Task.Delay(2000, cancellationToken);
        await _automationsLauncher.LaunchAutomationAsync(id, new FactsDictionary());
    }
}
