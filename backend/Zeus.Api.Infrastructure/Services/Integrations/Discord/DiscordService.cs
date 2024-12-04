using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

using ErrorOr;

using Zeus.Api.Application.Interfaces.Services.Integrations.Discord;
using Zeus.Api.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Api.Domain.Authentication.ValueObjects;
using Zeus.Api.Domain.Errors.Integrations;
using Zeus.Api.Domain.Integrations.Discord.ValueObjects;
using Zeus.Api.Infrastructure.Services.Integrations.Discord.Contracts;

namespace Zeus.Api.Infrastructure.Services.Integrations.Discord;

public class DiscordService : IDiscordService
{
    private readonly HttpClient _httpClient;
    private readonly IIntegrationsSettingsProvider _integrationsSettingsProvider;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public DiscordService(HttpClient httpClient, IIntegrationsSettingsProvider integrationsSettingsProvider)
    {
        _integrationsSettingsProvider = integrationsSettingsProvider;
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };

        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(_integrationsSettingsProvider.Discord.ApiEndpoint);
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    private AuthenticationHeaderValue GetAuthHeaderClientValue => new AuthenticationHeaderValue(
        "Basic",
        Convert.ToBase64String(Encoding.UTF8.GetBytes(
            $"{_integrationsSettingsProvider.Discord.ClientId}:{_integrationsSettingsProvider.Discord.ClientSecret}")));

    public async Task<ErrorOr<DiscordUserTokens>> GetTokensFromOauth2Async(string code)
    {
        _httpClient.DefaultRequestHeaders.Authorization = GetAuthHeaderClientValue;
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("oauth2/token",
            new DiscordOauth2TokenRequest(
                "authorization_code",
                code,
                _integrationsSettingsProvider.Discord.RedirectUrl
            ), _jsonSerializerOptions);

        if (!response.IsSuccessStatusCode)
            return Errors.Integrations.Discord.InvalidMethod;

        var responseContent =
            await response.Content.ReadFromJsonAsync<DiscordOauth2TokenResponse>(_jsonSerializerOptions);
        if (responseContent is null)
            return Errors.Integrations.Discord.InvalidBody;

        return new DiscordUserTokens(
            new AccessToken(responseContent.AccessToken),
            new RefreshToken(responseContent.RefreshToken),
            responseContent.TokenType,
            responseContent.ExpiresIn,
            responseContent.ExpiresIn);
    }
}
