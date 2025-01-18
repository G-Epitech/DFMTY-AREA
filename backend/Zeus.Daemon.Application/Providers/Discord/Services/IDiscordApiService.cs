using Zeus.Daemon.Domain.Discord.ValueObjects;

namespace Zeus.Daemon.Application.Providers.Discord.Services;

public interface IDiscordApiService
{
    public Task<bool> SendChannelMessageAsync(DiscordChannelId channelId, string message,
        CancellationToken cancellationToken = default);
}
