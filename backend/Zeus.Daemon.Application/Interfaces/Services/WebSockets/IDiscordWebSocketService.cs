using System.Text.Json.Nodes;

using Zeus.Daemon.Domain.Discord.Enums;

namespace Zeus.Daemon.Application.Interfaces.Services.Websockets;

public interface IDiscordWebSocketService
{
    public void Connect();
    public void Register(DiscordGatewayEventType eventType, Func<JsonNode, CancellationToken, Task> handler);
}
