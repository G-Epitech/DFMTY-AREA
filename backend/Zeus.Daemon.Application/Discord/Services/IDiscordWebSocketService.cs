using System.Text.Json.Nodes;

using Zeus.Daemon.Application.Interfaces;
using Zeus.Daemon.Domain.Discord.Enums;

namespace Zeus.Daemon.Application.Discord.Services;

public interface IDiscordWebSocketService : IDaemonService
{
    public Task ConnectAsync(CancellationToken cancellationToken);
    public void Register(DiscordGatewayEventType eventType, Func<JsonNode, CancellationToken, Task> handler);
}
