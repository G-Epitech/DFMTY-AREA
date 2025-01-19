using Zeus.Daemon.Domain.Providers.Discord.ValueObjects;

namespace Zeus.Daemon.Application.Providers.Discord.Services;

public interface IDiscordApiService
{
    public Task<bool> SendChannelMessageAsync(DiscordChannelId channelId, string message,
        CancellationToken cancellationToken = default);

    public Task<bool> SendChannelEmbedAsync(DiscordChannelId channelId, string title, string description, int color,
        string imageUrl,
        CancellationToken cancellationToken = default);

    public Task<bool> SendChannelEmbedFromJsonAsync(DiscordChannelId channelId, string json,
        CancellationToken cancellationToken = default);
}
