using System.Text.Json;
using System.Text.Json.Nodes;

using Zeus.Daemon.Application.Abstracts;
using Zeus.Daemon.Application.Discord.Services.Websocket;
using Zeus.Daemon.Domain.Automation;
using Zeus.Daemon.Domain.Discord.Enums;
using Zeus.Daemon.Domain.Discord.Events;

namespace Zeus.Daemon.Application.Discord.Triggers;

public class DiscordMessageReceivedTriggerHandler : TriggerHandler
{
    private readonly IDiscordWebSocketService _discordWebSocketService;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private AutomationExecutionContext? _context;

    public DiscordMessageReceivedTriggerHandler(IDiscordWebSocketService discordWebSocketService,
        IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _discordWebSocketService = discordWebSocketService;
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };
        _context = null;
    }

    public override Task HandleAsync(AutomationExecutionContext context, CancellationToken cancellationToken)
    {
        _context = context;
        _discordWebSocketService.Register(DiscordGatewayEventType.MessageCreate, HandleEvent);

        return Task.CompletedTask;
    }
    
    public override Task CancelAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Cancelling DiscordMessageReceivedTriggerHandler.");
        _context = null;
        return Task.CompletedTask;
    }

    private async Task HandleEvent(JsonNode data, CancellationToken cancellationToken)
    {
        if (_context is null)
        {
            return;
        }

        var messageCreate = JsonSerializer.Deserialize<MessageCreate>(data.ToJsonString(), _jsonSerializerOptions);
        if (messageCreate is null)
        {
            return;
        }

        var parameters = _context.Automation.Trigger.Parameters;
        var guildId = parameters.FirstOrDefault(p => p.Identifier == "GuildId")?.Value ??
                      String.Empty;
        var channelId = parameters.FirstOrDefault(p => p.Identifier == "ChannelId")?.Value ??
                        String.Empty;

        if (messageCreate.GuildId != guildId ||
            messageCreate.ChannelId != channelId ||
            messageCreate.Author.Bot == true)
        {
            return;
        }

        var facts = new Dictionary<string, string>
        {
            { "MessageId", messageCreate.Id },
            { "Content", messageCreate.Content },
            { "SenderId", messageCreate.Author.Id },
            { "SenderUsername", messageCreate.Author.Username },
            { "ReceptionTime", DateTimeOffset.Parse(messageCreate.Timestamp).ToString("o") }
        };

        Console.WriteLine($"React to message '{messageCreate.Id}' from '{messageCreate.Author.Username}'.");
        await this.ExecuteAsync(_context, facts, cancellationToken);
        Console.WriteLine($"Reacted to message '{messageCreate.Id}' from '{messageCreate.Author.Username}'.");
    }
}
