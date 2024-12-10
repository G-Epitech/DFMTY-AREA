using Microsoft.Extensions.DependencyInjection;

using Zeus.Daemon.Application.Discord.Services.Websocket;
using Zeus.Api.gRPC.SDK.Services;
using Zeus.Daemon.Application.Discord.Triggers;
using Zeus.Daemon.Domain.Automation;
using Zeus.Daemon.Domain.Automation.AutomationAggregate;
using Zeus.Daemon.Domain.Automation.AutomationAggregate.Entities;
using Zeus.Daemon.Domain.Automation.AutomationAggregate.Enums;
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

    private async Task HasNewAutomations()
    {
        var lastUpdate = DateTime.UnixEpoch;
        var grpc = _serviceProvider.GetRequiredService<SynchronizationGrpcService>();
        
        while (true)
        {
            var hasChanges = await grpc.HasChangesAsync(lastUpdate, CancellationToken.None);
            if (hasChanges)
            {
                Console.WriteLine("Changes detected!");
                lastUpdate = DateTime.UtcNow;
            }

            await Task.Delay(1000);
        }
    }

    /**
     * To use dependency injection in a class that is not registered in the service collection,
     * you can use the ActivatorUtilities class, like so:
     * ActivatorUtilities.CreateInstance<IService>(_serviceProvider);
     */
    public async Task Run()
    {
        var discord = _serviceProvider.GetRequiredService<IDiscordWebSocketService>();
        var triggerHandler = ActivatorUtilities.CreateInstance<DiscordMessageReceivedTriggerHandler>(_serviceProvider);

        var task = discord.ConnectAsync();

        var triggerParameters = new List<AutomationTriggerParameter>
        {
            new AutomationTriggerParameter { Value = "1316046870178697267", Identifier = "GuildId" },
            new AutomationTriggerParameter { Value = "1316046972733620244", Identifier = "ChannelId" },
        };
        var trigger = AutomationTrigger.Create("DiscordMessageReceived", triggerParameters, new List<IntegrationId>());

        var actionParameters = new List<AutomationActionParameter>
        {
            new AutomationActionParameter
            {
                Value = "1316046972733620244",
                Identifier = "ChannelId",
                Type = AutomationActionParameterType.Raw
            },
            new AutomationActionParameter
            {
                Value = "C'est la fête mes loulous",
                Identifier = "Content",
                Type = AutomationActionParameterType.Raw
            },
        };
        var actions = new List<AutomationAction>
        {
            AutomationAction.Create("Discord.SendMessageToChannel", 0, actionParameters, new List<IntegrationId>())
        };

        AutomationExecutionContext context = new(
            Automation.Create("Test", "test description", new UserId(Guid.NewGuid()), trigger,
                actions),
            new List<Integration>()
        );

        await triggerHandler.HandleAsync(context, CancellationToken.None);

        await task;
    }
}
