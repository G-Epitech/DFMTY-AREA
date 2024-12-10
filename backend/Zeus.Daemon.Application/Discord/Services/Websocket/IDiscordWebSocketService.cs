using System.Text.Json.Nodes;

using Zeus.Daemon.Domain.Discord.Enums;

namespace Zeus.Daemon.Application.Discord.Services.Websocket;

public interface IDiscordWebSocketService
{
    public Task ConnectAsync();
    public void Register(DiscordGatewayEventType eventType, Func<JsonNode, CancellationToken, Task> handler);
}
