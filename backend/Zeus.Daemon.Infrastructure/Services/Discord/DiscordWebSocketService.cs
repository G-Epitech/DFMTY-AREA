using System.Net.WebSockets;
using System.Text;
using System.Text.Json.Nodes;

using Zeus.Daemon.Application.Discord.Services;
using Zeus.Daemon.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Daemon.Domain.Discord.Enums;

namespace Zeus.Daemon.Infrastructure.Services.Discord;

public class DiscordWebSocketService : IDiscordWebSocketService
{
    private readonly IIntegrationsSettingsProvider _integrationsSettingsProvider;
    private readonly ClientWebSocket _webSocket;
    private CancellationToken? _cancellationToken;

    private int _heartbeatInterval;

    private readonly List<(DiscordGatewayEventType EventType, Func<JsonNode, CancellationToken, Task> Handler)>
        _eventHandlers;

    public DiscordWebSocketService(IIntegrationsSettingsProvider integrationsSettingsProvider)
    {
        _integrationsSettingsProvider = integrationsSettingsProvider;
        _webSocket = new ClientWebSocket();
        _heartbeatInterval = 0;
        _eventHandlers = new List<(DiscordGatewayEventType, Func<JsonNode, CancellationToken, Task>)>();
    }

    public async Task ConnectAsync(CancellationToken cancellationToken)
    {
        var uri = new Uri(_integrationsSettingsProvider.Discord.WebsocketEndpoint + "?v=10&encoding=json");

        _cancellationToken = cancellationToken;
        await _webSocket.ConnectAsync(uri, _cancellationToken ?? CancellationToken.None);
        Console.WriteLine("WebSocket connected to Discord.");

        await ListenAsync(_cancellationToken ?? CancellationToken.None);
    }

    private async Task ListenAsync(CancellationToken cancellationToken)
    {
        var buffer = new byte[8192];
        while (_webSocket.State == WebSocketState.Open)
        {
            var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                Console.WriteLine("WebSocket disconnected.");
                await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", cancellationToken);
                break;
            }

            if (result.MessageType == WebSocketMessageType.Text)
            {
                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                _ = OnMessage(message);
            }
        }
    }

    private async Task OnMessage(string message)
    {
        var json = JsonNode.Parse(message);
        if (json is null)
        {
            return;
        }

        var opCode = (int?)json["op"];

        switch (opCode)
        {
            case 10:
                _heartbeatInterval = (int?)json["d"]?["heartbeat_interval"] ?? 0;
                StartHeartbeat();
                await Identify();
                break;

            case 0:
                HandleDispatchEvent(json);
                break;
        }
    }

    private void StartHeartbeat()
    {
        _ = new Timer(SendHeartbeat, null, 0, _heartbeatInterval);
    }

    private void SendHeartbeat(object? state)
    {
        var heartbeatPayload = new JsonObject { ["op"] = 1, ["d"] = null };

        _ = SendAsync(heartbeatPayload.ToString());
    }

    private async Task Identify()
    {
        var identifyPayload = new JsonObject
        {
            ["op"] = 2,
            ["d"] = new JsonObject
            {
                ["token"] = _integrationsSettingsProvider.Discord.BotToken,
                ["intents"] = 33281,
                ["properties"] = new JsonObject
                {
                    ["os"] = "linux", ["browser"] = "native-websocket", ["device"] = "native-websocket"
                }
            }
        };

        await SendAsync(identifyPayload.ToString());
    }

    private void HandleDispatchEvent(JsonNode json)
    {
        var eventName = (string?)json["t"] ?? string.Empty;
        var data = json["d"];

        switch (eventName)
        {
            case "MESSAGE_CREATE":
                {
                    _eventHandlers
                        .Where(x => x.EventType == DiscordGatewayEventType.MessageCreate)
                        .ToList()
                        .ForEach(x => x.Handler(data ?? new JsonObject(), CancellationToken.None));
                }
                break;
        }
    }

    public void Register(DiscordGatewayEventType eventType, Func<JsonNode, CancellationToken, Task> handler)
    {
        _eventHandlers.Add((eventType, handler));
    }

    private async Task SendAsync(string message)
    {
        var messageBytes = Encoding.UTF8.GetBytes(message);
        await _webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true,
            _cancellationToken ?? CancellationToken.None);
    }
}
