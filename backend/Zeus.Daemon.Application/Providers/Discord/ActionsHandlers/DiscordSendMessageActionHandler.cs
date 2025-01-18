using Microsoft.Extensions.Logging;

using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Domain.Integrations.IntegrationAggregate;
using Zeus.Daemon.Application.Attributes;
using Zeus.Daemon.Application.Execution;
using Zeus.Daemon.Application.Providers.Discord.Services;
using Zeus.Daemon.Domain.Automations;
using Zeus.Daemon.Domain.Discord.ValueObjects;

namespace Zeus.Daemon.Application.Providers.Discord.ActionsHandlers;

public class DiscordSendMessageActionHandler
{
    private readonly IDiscordApiService _discordApiService;

    public DiscordSendMessageActionHandler(IDiscordApiService discordApiService)
    {
        _discordApiService = discordApiService;
    }

    [ActionHandler("Discord.SendMessageToChannel")]
    public async Task<ActionResult> RunAsync(
        AutomationId automationId,
        [FromParameters] string channelId,
        [FromParameters] string content,
        ILogger logger,
        CancellationToken cancellationToken
    )
    {
        // TODO: Check permissions
        logger.LogInformation("Sending message to channel {ChannelId}", channelId);
        await _discordApiService.SendChannelMessageAsync(new DiscordChannelId(channelId), content, cancellationToken);
        return new FactsDictionary();
    }
}
