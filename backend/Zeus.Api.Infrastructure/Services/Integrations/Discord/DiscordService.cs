using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

using ErrorOr;

using Zeus.Api.Application.Interfaces.Services.Integrations.Discord;
using Zeus.Api.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Api.Domain.Authentication.ValueObjects;
using Zeus.Api.Domain.Errors.Integrations;
using Zeus.Api.Domain.Integrations.Discord;
using Zeus.Api.Domain.Integrations.Discord.ValueObjects;
using Zeus.Api.Infrastructure.Services.Integrations.Discord.Contracts;

namespace Zeus.Api.Infrastructure.Services.Integrations.Discord;

public class DiscordService : IDiscordService
{
    private readonly HttpClient _httpClient;
    private readonly IIntegrationsSettingsProvider _integrationsSettingsProvider;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public DiscordService(IIntegrationsSettingsProvider integrationsSettingsProvider)
    {
        _integrationsSettingsProvider = integrationsSettingsProvider;
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };

        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(_integrationsSettingsProvider.Discord.ApiEndpoint);
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
    }

    private AuthenticationHeaderValue GetAuthHeaderClientValue => new AuthenticationHeaderValue(
        "Basic",
        Convert.ToBase64String(Encoding.UTF8.GetBytes(
            $"{_integrationsSettingsProvider.Discord.ClientId}:{_integrationsSettingsProvider.Discord.ClientSecret}")));

    private static AuthenticationHeaderValue GetAuthHeaderBearerValue(AccessToken accessToken) =>
        new AuthenticationHeaderValue(
            "Bearer",
            accessToken.Value);

    private static Uri GetUserAvatarUri(ulong userId, string? avatarHash)
    {
        if (avatarHash is null)
        {
            var index = (userId >> 22) % 6;
            return new Uri($"https://cdn.discordapp.com/embed/avatars/{index}.png");
        }

        return new Uri($"https://cdn.discordapp.com/avatars/{userId}/{avatarHash}.png");
    }

    public async Task<ErrorOr<DiscordUserTokens>> GetTokensFromOauth2Async(string code)
    {
        _httpClient.DefaultRequestHeaders.Authorization = GetAuthHeaderClientValue;

        var requestContent = new FormUrlEncodedContent([
            new KeyValuePair<string, string>("grant_type", "authorization_code"),
            new KeyValuePair<string, string>("code", code),
            new KeyValuePair<string, string>("redirect_uri", _integrationsSettingsProvider.Discord.RedirectUrl)
        ]);

        HttpResponseMessage response = await _httpClient.PostAsync("oauth2/token", requestContent);

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
            responseContent.ExpiresIn);
    }

    public async Task<ErrorOr<DiscordUser>> GetUserAsync(AccessToken accessToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = GetAuthHeaderBearerValue(accessToken);
        HttpResponseMessage response = await _httpClient.GetAsync("users/@me");

        if (response.StatusCode == HttpStatusCode.Unauthorized)
            return Errors.Integrations.Discord.InvalidDiscordUserCredentials;
        if (!response.IsSuccessStatusCode)
            return Errors.Integrations.Discord.InvalidMethod;

        var responseContent = await response.Content.ReadFromJsonAsync<GetDiscordUserResponse>(_jsonSerializerOptions);
        if (responseContent is null)
            return Errors.Integrations.Discord.InvalidBody;

        var discordUserIdId = new DiscordUserId(responseContent.Id);
        var avatar = GetUserAvatarUri(discordUserIdId.Value, responseContent.Avatar);

        return DiscordUser.Create(
            discordUserIdId,
            responseContent.Username,
            responseContent.Email,
            responseContent.GlobalName ?? responseContent.Username,
            avatar,
            []);
    }
}
