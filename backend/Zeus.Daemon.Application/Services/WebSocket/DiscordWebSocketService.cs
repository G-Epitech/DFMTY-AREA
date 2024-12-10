using System.Net.WebSockets;
using System.Text;
using System.Text.Json.Nodes;

using Zeus.Daemon.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Daemon.Application.Interfaces.Services.WebSockets;
using Zeus.Daemon.Domain.Discord.Enums;

namespace Zeus.Daemon.Application.Services.WebSocket;

public class DiscordWebSocketService : IDiscordWebSocketService
{
    private readonly IIntegrationsSettingsProvider _integrationsSettingsProvider;
    private readonly ClientWebSocket _webSocket;
    private readonly CancellationTokenSource _cancellationTokenSource;

    private int _heartbeatInterval;

    private readonly List<(DiscordGatewayEventType EventType, Func<JsonNode, CancellationToken, Task> Handler)>
        _eventHandlers;

    public DiscordWebSocketService(IIntegrationsSettingsProvider integrationsSettingsProvider)
    {
        _integrationsSettingsProvider = integrationsSettingsProvider;
        _webSocket = new ClientWebSocket();
        _cancellationTokenSource = new CancellationTokenSource();
        _heartbeatInterval = 0;
        _eventHandlers = new List<(DiscordGatewayEventType, Func<JsonNode, CancellationToken, Task>)>();
    }

    public async Task ConnectAsync()
    {
        var uri = new Uri(_integrationsSettingsProvider.Discord.WebsocketEndpoint + "?v=10&encoding=json");

        await _webSocket.ConnectAsync(uri, _cancellationTokenSource.Token);
        Console.WriteLine("WebSocket connected to Discord.");

        await ListenAsync(_cancellationTokenSource.Token);
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
                _eventHandlers.FirstOrDefault(e => e.EventType == DiscordGatewayEventType.MessageCreate)
                    .Handler(data ?? new JsonObject(), CancellationToken.None); break;
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
            _cancellationTokenSource.Token);
    }
}
