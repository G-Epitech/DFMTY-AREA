using System.Net.WebSockets;

using Zeus.Daemon.Application.Abstracts;
using Zeus.Daemon.Domain.Automation;

namespace Zeus.Daemon.Application.Discord.Triggers;

public class DiscordMessageReceived : TriggerHandler
{
    private readonly ClientWebSocket _webSocket;

    public DiscordMessageReceived(ClientWebSocket webSocket)
    {
        _webSocket = webSocket;
    }

    public override async Task HandleAsync(AutomationExecutionContext context, CancellationToken cancellationToken)
    {
        await _webSocket.ConnectAsync(new Uri("wss://gateway.discord.gg"), cancellationToken);
    }

    public override Task ListenAsync(AutomationExecutionContext context, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
