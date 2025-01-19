using Zeus.Daemon.Application.Attributes;
using Zeus.Daemon.Application.Execution;
using Zeus.Daemon.Application.Providers.Discord.Services;
using Zeus.Daemon.Domain.Automations;
using Zeus.Daemon.Domain.Providers.Discord.ValueObjects;

namespace Zeus.Daemon.Application.Providers.Discord.ActionsHandlers;

public class DiscordSendEmbedJsonActionHandler
{
    private readonly IDiscordApiService _discordApiService;

    public DiscordSendEmbedJsonActionHandler(IDiscordApiService discordApiService)
    {
        _discordApiService = discordApiService;
    }

    [ActionHandler("Discord.SendEmbedFromJsonToChannel")]
    public async Task<ActionResult> RunAsync(
        [FromParameters] string channelId,
        [FromParameters] string json,
        CancellationToken cancellationToken
    )
    {
        Console.WriteLine(json);
        try
        {
            await _discordApiService.SendChannelEmbedFromJsonAsync(new DiscordChannelId(channelId), json,
                cancellationToken);
            return new FactsDictionary();
        }
        catch (Exception ex)
        {
            return new ActionError
            {
                Details = ex, InnerException = ex, Message = "An error occurred while sending the embed from json"
            };
        }
    }
}
