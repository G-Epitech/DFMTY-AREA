using Zeus.Daemon.Application.Discord.Services.Api;
using Zeus.Daemon.Application.Interfaces;
using Zeus.Daemon.Domain.Automation.AutomationAggregate.Entities;
using Zeus.Daemon.Domain.Discord.ValueObjects;
using Zeus.Daemon.Domain.IntegrationAggregate;

namespace Zeus.Daemon.Application.Discord.Actions;

public class DiscordSendMessageActionHandler : IActionHandler
{
    private readonly IDiscordApiService _discordApiService;

    public DiscordSendMessageActionHandler(IDiscordApiService discordApiService)
    {
        _discordApiService = discordApiService;
    }

    public Task HandleAsync(AutomationAction action, IReadOnlyCollection<Integration> integrations,
        Dictionary<string, string> facts,
        CancellationToken cancellationToken)
    {
        var channelId = action.Parameters.FirstOrDefault(p => p.Identifier == "ChannelId")?.Value;
        var content = action.Parameters.FirstOrDefault(p => p.Identifier == "Content")?.Value;

        if (channelId is null || content is null)
        {
            return Task.CompletedTask;
        }

        // TODO: Check permissions

        return _discordApiService.SendChannelMessageAsync(new DiscordChannelId(channelId), content, cancellationToken);
    }
}
