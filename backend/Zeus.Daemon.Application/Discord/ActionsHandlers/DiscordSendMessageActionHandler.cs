using System.Diagnostics.CodeAnalysis;

using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
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
        [FromParameters] string channelId,
        [FromParameters] string content,
        CancellationToken cancellationToken
    )
    {
        // TODO: Check permissions
        Console.WriteLine($"[{automationId.Value.ToString()}] We are here with channelId: {channelId} and content: {content}");
        await _discordApiService.SendChannelMessageAsync(new DiscordChannelId(channelId), content, cancellationToken);
        return new FactsDictionary();
    }
}
