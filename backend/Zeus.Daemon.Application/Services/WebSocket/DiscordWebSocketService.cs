using System.Text.Json.Nodes;

using Zeus.Daemon.Application.Interfaces.Services.Websockets;
using Zeus.Daemon.Domain.Discord.Enums;

namespace Zeus.Daemon.Application.Services.Websocket;

public class DiscordWebSocketService : IDiscordWebSocketService
{
    public void Connect()
    {
        throw new NotImplementedException();
    }

    public void Register(DiscordGatewayEventType eventType, Func<JsonObject, CancellationToken, Task> handler)
    {
        throw new NotImplementedException();
    }
}
