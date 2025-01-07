using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.AutomationAggregate.Entities;
using Zeus.Daemon.Application.Abstracts;
using Zeus.Daemon.Application.Attributes;
using Zeus.Daemon.Application.Discord.Services.Websocket;

namespace Zeus.Daemon.Application.Discord.TriggerHandlers;

[TriggerIdentifier("Discord.MessageReceivedInChannel")]
public class DiscordMessageReceivedTriggerHandler : TriggerHandler
{
    public DiscordMessageReceivedTriggerHandler(IDiscordWebSocketService discordWebSocketService) {}

    protected override Task<bool> OnRegisterAsync(Automation automation, CancellationToken cancellationToken = default)
    {
        Console.WriteLine("DiscordMessageReceivedTriggerHandler.OnRegisterAsync");
        return base.OnRegisterAsync(automation, cancellationToken);
    }

    protected override Task<bool> OnRemoveAsync(AutomationTrigger trigger, CancellationToken cancellationToken = default)
    {
        Console.WriteLine("DiscordMessageReceivedTriggerHandler.OnRemoveAsync");
        return base.OnRemoveAsync(trigger, cancellationToken);
    }
}
