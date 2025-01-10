using System.Text.Json;
using System.Text.Json.Nodes;

using Zeus.Daemon.Application.Discord.Services;
using Zeus.Daemon.Application.Execution;
using Zeus.Daemon.Domain.Discord.Enums;

namespace Zeus.Daemon.Application.Discord.TriggerHandlers;

public class TriggerHandler_old
{
    private readonly IDiscordWebSocketService _discordWebSocketService;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private AutomationExecutionContext? _context;

    public TriggerHandler_old(IDiscordWebSocketService discordWebSocketService,
        IServiceProvider serviceProvider)
    {
        _discordWebSocketService = discordWebSocketService;
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };
        _context = null;
    }

    public Task HandleAsync(AutomationExecutionContext context, CancellationToken cancellationToken)
    {
        _context = context;
        _discordWebSocketService.Register(DiscordGatewayEventType.MessageCreate, HandleEvent);

        return Task.CompletedTask;
    }

    public Task CancelAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Cancelling DiscordMessageReceivedTriggerHandler.");
        _context = null;
        return Task.CompletedTask;
    }

    private async Task HandleEvent(JsonNode data, CancellationToken cancellationToken)
    {
        
    }
}
