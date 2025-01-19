using Zeus.Daemon.Domain.Providers.Discord.ValueObjects;

namespace Zeus.Daemon.Application.Providers.Discord.Services;

public interface IDiscordApiService
{
    public Task<bool> SendChannelMessageAsync(DiscordChannelId channelId, string message,
        CancellationToken cancellationToken = default);
}
