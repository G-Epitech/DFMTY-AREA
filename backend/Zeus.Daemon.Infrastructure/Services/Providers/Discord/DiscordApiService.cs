using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

using Microsoft.Extensions.Logging;

using Zeus.Daemon.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Daemon.Application.Providers.Discord.Services;
using Zeus.Daemon.Domain.Providers.Discord.ValueObjects;

namespace Zeus.Daemon.Infrastructure.Services.Providers.Discord;

public class DiscordApiService : IDiscordApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;

    public DiscordApiService(IIntegrationsSettingsProvider integrationsSettingsProvider,
        ILogger<DiscordApiService> logger)
    {
        _logger = logger;

        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(integrationsSettingsProvider.Discord.ApiEndpoint);

        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bot", integrationsSettingsProvider.Discord.BotToken);
    }

    public async Task<bool> SendChannelMessageAsync(DiscordChannelId channelId,
        string message,
        CancellationToken cancellationToken)
    {
        var requestContent = new FormUrlEncodedContent([
            new KeyValuePair<string, string>("content", message),
        ]);

        HttpResponseMessage response =
            await _httpClient.PostAsync($"channels/{channelId.Value}/messages", requestContent, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Failed to send message to Discord channel {ChannelId}.", channelId.Value);
        }

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SendChannelEmbedAsync(DiscordChannelId channelId, string title, string description,
        int color, string imageUrl,
        CancellationToken cancellationToken = default)
    {
        var requestContent = new JsonObject
        {
            ["content"] = "",
            ["embeds"] = new JsonArray
            {
                new JsonObject
                {
                    ["title"] = title,
                    ["description"] = description,
                    ["color"] = color,
                    ["image"] = new JsonObject { ["url"] = imageUrl, },
                },
            }
        };

        HttpResponseMessage response =
            await _httpClient.PostAsJsonAsync($"channels/{channelId.Value}/messages", requestContent,
                cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Failed to send message to Discord channel {ChannelId}.", channelId.Value);
        }

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SendChannelEmbedFromJsonAsync(DiscordChannelId channelId, string json,
        CancellationToken cancellationToken = default)
    {
        var requestContent = new JsonObject
        {
            ["content"] = "",
            ["embeds"] = JsonNode.Parse(json),
        };

        HttpResponseMessage response =
            await _httpClient.PostAsJsonAsync($"channels/{channelId.Value}/messages", requestContent,
                cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Failed to send message to Discord channel {ChannelId}.", channelId.Value);
        }

        return response.IsSuccessStatusCode;
    }
}
