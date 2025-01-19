using Zeus.Daemon.Application.Attributes;
using Zeus.Daemon.Application.Execution;
using Zeus.Daemon.Application.Providers.Discord.Services;
using Zeus.Daemon.Domain.Automations;
using Zeus.Daemon.Domain.Providers.Discord.ValueObjects;

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
        [FromParameters] string channelId,
        [FromParameters] string content,
        CancellationToken cancellationToken
    )
    {
        try
        {
            await _discordApiService.SendChannelMessageAsync(new DiscordChannelId(channelId), content, cancellationToken);
            return new FactsDictionary();
        }
        catch (Exception ex)
        {
            return new ActionError { Details = ex, InnerException = ex, Message = "An error occurred while sending the message" };
        }
    }
}
