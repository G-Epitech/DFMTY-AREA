using System.Text.Json.Nodes;

using Zeus.Daemon.Domain.Providers.Discord.Enums;

namespace Zeus.Daemon.Application.Providers.Discord.Services;

public interface IDiscordWebSocketService
{
    public void Register(DiscordGatewayEventType eventType, Func<JsonNode, CancellationToken, Task> handler);
}
