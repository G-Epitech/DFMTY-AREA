using Zeus.Daemon.Application.Attributes;
using Zeus.Daemon.Application.Execution;
using Zeus.Daemon.Application.Providers.Discord.Services;
using Zeus.Daemon.Domain.Automations;
using Zeus.Daemon.Domain.Providers.Discord.ValueObjects;

namespace Zeus.Daemon.Application.Providers.Discord.ActionsHandlers;

public class DiscordSendEmbedActionHandler
{
    private readonly IDiscordApiService _discordApiService;

    public DiscordSendEmbedActionHandler(IDiscordApiService discordApiService)
    {
        _discordApiService = discordApiService;
    }

    [ActionHandler("Discord.SendEmbedToChannel")]
    public async Task<ActionResult> RunAsync(
        [FromParameters] string channelId,
        [FromParameters] string title,
        [FromParameters] string description,
        [FromParameters] int color,
        [FromParameters] string imageUrl,
        CancellationToken cancellationToken
    )
    {
        try
        {
            await _discordApiService.SendChannelEmbedAsync(new DiscordChannelId(channelId), title, description, color,
                imageUrl, cancellationToken);
            return new FactsDictionary();
        }
        catch (Exception ex)
        {
            return new ActionError
            {
                Details = ex, InnerException = ex, Message = "An error occurred while sending the embed"
            };
        }
    }
}
