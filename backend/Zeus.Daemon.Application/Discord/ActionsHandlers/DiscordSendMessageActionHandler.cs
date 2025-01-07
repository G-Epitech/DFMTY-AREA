using System.Diagnostics.CodeAnalysis;

using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Domain.Integrations.IntegrationAggregate;
using Zeus.Daemon.Application.Attributes;
using Zeus.Daemon.Application.Discord.Services;
using Zeus.Daemon.Domain.Automations;
using Zeus.Daemon.Domain.Discord.ValueObjects;

namespace Zeus.Daemon.Application.Discord.ActionsHandlers;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)]
public class DiscordSendMessageActionHandler
{
    private readonly IDiscordApiService _discordApiService;

    public DiscordSendMessageActionHandler(IDiscordApiService discordApiService)
    {
        _discordApiService = discordApiService;
    }

    [ActionHandler("Discord.SendMessageToChannel")]
    public async Task<FactsDictionary> RunAsync(
        AutomationId automationId,
        [FromParameter] string channelId,
        [FromParameter] string content,
        CancellationToken cancellationToken
    )
    {
        // TODO: Check permissions
        Console.WriteLine($"[{automationId.Value.ToString()}] We are here");

        await _discordApiService.SendChannelMessageAsync(new DiscordChannelId(channelId), content, cancellationToken);
        return new FactsDictionary();
    }
}
