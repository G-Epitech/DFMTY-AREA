using ErrorOr;

using Zeus.Daemon.Domain.Discord.ValueObjects;

namespace Zeus.Daemon.Application.Discord.Services.Api;

public interface IDiscordApiService
{
    public Task<ErrorOr<Dictionary<string, string>>> SendChannelMessageAsync(DiscordChannelId channelId, string message,
        CancellationToken cancellationToken = default);
}
