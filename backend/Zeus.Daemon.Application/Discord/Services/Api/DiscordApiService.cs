using System.Net.Http.Headers;

using ErrorOr;

using Zeus.Daemon.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Daemon.Domain.Discord.ValueObjects;
using Zeus.Daemon.Domain.Errors.Services;

namespace Zeus.Daemon.Application.Discord.Services.Api;

public class DiscordApiService : IDiscordApiService
{
    private readonly HttpClient _httpClient;

    public DiscordApiService(IIntegrationsSettingsProvider integrationsSettingsProvider)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(integrationsSettingsProvider.Discord.ApiEndpoint);

        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bot", integrationsSettingsProvider.Discord.BotToken);
    }

    public async Task<ErrorOr<Dictionary<string, string>>> SendChannelMessageAsync(DiscordChannelId channelId,
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
            Console.WriteLine("Failed to send message to Discord.");
            return Errors.Services.Discord.FailureDuringRequest;
        }

        return new Dictionary<string, string>();
    }
}
