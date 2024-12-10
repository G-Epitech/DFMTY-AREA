using System.Text.Json.Nodes;

using WebSocketSharp;

using Zeus.Daemon.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Daemon.Application.Interfaces.Services.Websockets;
using Zeus.Daemon.Domain.Discord.Enums;

namespace Zeus.Daemon.Application.Services.Websocket;

public class DiscordWebSocketService : IDiscordWebSocketService
{
    private readonly IIntegrationsSettingsProvider _integrationsSettingsProvider;
    private readonly WebSocket _webSocket;

    private int _heartbeatInterval;
    private Timer? _heartbeatTimer;

    private List<(DiscordGatewayEventType EventType, Func<JsonNode, CancellationToken, Task> Handler)> _eventHandlers;

    public DiscordWebSocketService(IIntegrationsSettingsProvider integrationsSettingsProvider)
    {
        _integrationsSettingsProvider = integrationsSettingsProvider;
        _webSocket = new WebSocket(_integrationsSettingsProvider.Discord.WebsocketEndpoint + "?v=10&encoding=json");

        _heartbeatInterval = 0;
        _eventHandlers = new List<(DiscordGatewayEventType, Func<JsonNode, CancellationToken, Task>)>();
    }

    public void Connect()
    {
        _webSocket.OnMessage += (sender, e) => OnMessage(e.Data);
        _webSocket.OnOpen += (sender, e) => Console.WriteLine("WebSocket connected to Discord.");
        _webSocket.OnClose += (sender, e) => Console.WriteLine("WebSocket disconnected.");

        _webSocket.Connect();
    }

    private void OnMessage(string message)
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
                Identify();
                break;

            case 0:
                HandleDispatchEvent(json);
                break;
        }
    }

    private void StartHeartbeat()
    {
        _heartbeatTimer = new Timer(SendHeartbeat, null, 0, _heartbeatInterval);
    }

    private void SendHeartbeat(object? state)
    {
        var heartbeatPayload = new JsonObject { ["op"] = 1, ["d"] = null };

        _webSocket.Send(heartbeatPayload.ToString());
    }

    private void Identify()
    {
        var identifyPayload = new JsonObject
        {
            ["op"] = 2,
            ["d"] = new JsonObject
            {
                ["token"] = _integrationsSettingsProvider.Discord.ClientSecret,
                ["intents"] = 513,
                ["properties"] = new JsonObject
                {
                    ["os"] = "linux", ["browser"] = "websocket-sharp", ["device"] = "websocket-sharp"
                }
            }
        };

        _webSocket.Send(identifyPayload.ToString());
    }

    private void HandleDispatchEvent(JsonNode json)
    {
        var eventName = (string?)json["t"] ?? String.Empty;
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
}
