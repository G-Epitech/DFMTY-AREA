using Microsoft.Extensions.DependencyInjection;

using Zeus.Daemon.Application.Discord.Triggers;
using Zeus.Daemon.Application.Interfaces.Services.Websockets;
using Zeus.Daemon.Domain.Automation;
using Zeus.Daemon.Domain.Automation.AutomationAggregate;
using Zeus.Daemon.Domain.Automation.AutomationAggregate.Entities;
using Zeus.Daemon.Domain.Automation.AutomationAggregate.ValueObjects;
using Zeus.Daemon.Domain.IntegrationAggregate;
using Zeus.Daemon.Domain.IntegrationAggregate.ValueObjects;
using Zeus.Daemon.Domain.User.ValueObjects;

namespace Zeus.Daemon.Runner.Runner;

public class DaemonRunner
{
    private readonly IServiceProvider _serviceProvider;

    public DaemonRunner(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /**
     * To use dependency injection in a class that is not registered in the service collection,
     * you can use the ActivatorUtilities class, like so:
     * ActivatorUtilities.CreateInstance<IService>(_serviceProvider);
     */
    public async Task Run()
    {
        var discord = _serviceProvider.GetRequiredService<IDiscordWebSocketService>();
        var triggerHandler = ActivatorUtilities.CreateInstance<DiscordMessageReceivedTrigger>(_serviceProvider);

        var task = discord.ConnectAsync();

        var parameters = new List<DynamicParameter>
        {
            new DynamicParameter { Value = "965293637145591868", Identifier = "GuildId" },
            new DynamicParameter { Value = "965293637145591871", Identifier = "ChannelId" },
        };
        var trigger = AutomationTrigger.Create("DiscordMessageReceived", parameters, new List<IntegrationId>());

        AutomationExecutionContext context = new(
            Automation.Create("Test", "test description", new UserId(Guid.NewGuid()), trigger,
                new List<AutomationAction>()),
            new List<Integration>()
        );

        await triggerHandler.HandleAsync(context, CancellationToken.None);

        await task;
    }
}
