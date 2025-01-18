using System.Text.Json;
using System.Text.Json.Nodes;

using Microsoft.Extensions.Logging;

using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Domain.Integrations.IntegrationAggregate;
using Zeus.Daemon.Application.Attributes;
using Zeus.Daemon.Application.Interfaces;
using Zeus.Daemon.Application.Providers.Discord.Services;
using Zeus.Daemon.Domain.Automations;
using Zeus.Daemon.Domain.Providers.Discord.Enums;
using Zeus.Daemon.Domain.Providers.Discord.Events;

namespace Zeus.Daemon.Application.Providers.Discord.TriggerHandlers;

[TriggerHandler("Discord.MessageReceivedInChannel")]
public class DiscordMessageReceivedTriggerHandler
{
    private readonly IAutomationsLauncher _automationsLauncher;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly ILogger _logger;
    private readonly Dictionary<AutomationId, TriggerParameters> _triggers = new();

    public DiscordMessageReceivedTriggerHandler(
        IAutomationsLauncher automationsLauncher,
        IDiscordWebSocketService discordWebSocketService,
        ILogger<DiscordMessageReceivedTriggerHandler> logger
    )
    {
        _automationsLauncher = automationsLauncher;
        _logger = logger;
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };
        discordWebSocketService.Register(DiscordGatewayEventType.MessageCreate, OnMessageReceived);
    }

    [OnTriggerRegister]
    public Task<bool> OnRegisterAsync(
        AutomationId automationId,
        [FromParameters] string channelId,
        [FromParameters] string guildId,
        [FromIntegrations] DiscordIntegration integration,
        CancellationToken cancellationToken = default)
    {
        _triggers[automationId] = new TriggerParameters { GuildId = guildId, ChannelId = channelId };
        return Task.FromResult(true);
    }

    [OnTriggerRemove]
    public Task<bool> OnRemoveAsync(AutomationId automationId, CancellationToken cancellationToken = default)
    {
        _triggers.Remove(automationId);
        return Task.FromResult(true);
    }

    private async Task OnMessageReceived(JsonNode data, CancellationToken cancellationToken)
    {
        try
        {
            var messageCreate = JsonSerializer.Deserialize<MessageCreate>(data.ToJsonString(), _jsonSerializerOptions);
            if (messageCreate is null || messageCreate.Author.Bot == true || messageCreate.GuildId is null)
            {
                return;
            }

            var facts = new FactsDictionary
            {
                { "MessageId", Fact.Create(messageCreate.Id) },
                { "Content", Fact.Create(messageCreate.Content) },
                { "SenderId", Fact.Create(messageCreate.Author.Id) },
                { "SenderUsername", Fact.Create(messageCreate.Author.Username) },
                { "ReceptionTime", Fact.Create(DateTimeOffset.Parse(messageCreate.Timestamp).UtcDateTime) }
            };

            var received =
                new TriggerParameters { GuildId = messageCreate.GuildId, ChannelId = messageCreate.ChannelId };

            await LaunchTargetedAutomations(received, facts);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in DiscordMessageReceivedTriggerHandler.OnMessageReceived: {ex.Message}");
        }
    }

    private static bool TriggerIsTargeted(TriggerParameters targeted, TriggerParameters candidate)
    {
        return targeted.GuildId == candidate.GuildId && targeted.ChannelId == candidate.ChannelId;
    }

    private List<AutomationId> GetTargetedAutomations(TriggerParameters targeted)
    {
        return _triggers.Where(k => TriggerIsTargeted(targeted, k.Value)).Select(t => t.Key).ToList();
    }

    private async Task LaunchTargetedAutomations(TriggerParameters targeted, FactsDictionary facts
    )
    {
        var res = await _automationsLauncher.LaunchManyAsync(GetTargetedAutomations(targeted), facts);

        foreach ((AutomationId automationId, bool started) in res)
        {
            if (!started)
            {
                _logger.LogError("Automation {id} failed to launch", automationId.Value);
            }
        }
    }

    private struct TriggerParameters
    {
        public string GuildId { get; init; }
        public string ChannelId { get; init; }
    }
}
