using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.AutomationAggregate.Entities;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Daemon.Application.Abstracts;
using Zeus.Daemon.Application.Attributes;
using Zeus.Daemon.Application.Discord.Services;
using Zeus.Daemon.Domain.Automations;

namespace Zeus.Daemon.Application.Discord.TriggerHandlers;

[TriggerIdentifier("Discord.MessageReceivedInChannel")]
public class DiscordMessageReceivedTriggerHandler : TriggerHandler
{
    public DiscordMessageReceivedTriggerHandler(IDiscordWebSocketService discordWebSocketService, IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }

    protected override Task<bool> OnRegisterAsync(Automation automation, CancellationToken cancellationToken = default)
    {
        Console.WriteLine("DiscordMessageReceivedTriggerHandler.OnRegisterAsync");
        Task.Run(async () => await RunAfter2S(automation.Id, cancellationToken), cancellationToken);
        return base.OnRegisterAsync(automation, cancellationToken);
    }

    protected override Task<bool> OnRemoveAsync(AutomationTrigger trigger, CancellationToken cancellationToken = default)
    {
        Console.WriteLine("DiscordMessageReceivedTriggerHandler.OnRemoveAsync");
        return base.OnRemoveAsync(trigger, cancellationToken);
    }
    
    [OnTriggerRegister]
    protected async Task RunAfter2S(AutomationId id, CancellationToken cancellationToken = default)
    {
        Console.WriteLine("DiscordMessageReceivedTriggerHandler.RunAfter2s");
        await Task.Delay(2000, cancellationToken);
        await RunAutomationAsync(id, new FactsDictionary());
    }
}
