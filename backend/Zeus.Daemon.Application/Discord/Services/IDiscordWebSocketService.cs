using System.Text.Json.Nodes;

using Zeus.Daemon.Domain.Discord.Enums;

namespace Zeus.Daemon.Application.Discord.Services;

public interface IDiscordWebSocketService
{
    public void Register(DiscordGatewayEventType eventType, Func<JsonNode, CancellationToken, Task> handler);
}
