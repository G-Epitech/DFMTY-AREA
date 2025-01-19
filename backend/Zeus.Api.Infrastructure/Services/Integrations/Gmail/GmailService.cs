using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

using ErrorOr;

using Zeus.Api.Application.Interfaces.Services.Integrations;
using Zeus.Api.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Api.Domain.Errors.Integrations;
using Zeus.Api.Domain.Integrations.Gmail;
using Zeus.Api.Domain.Integrations.Gmail.ValueObjects;
using Zeus.Api.Infrastructure.Services.Integrations.Gmail.Contracts;
using Zeus.Common.Domain.Authentication.Common;

namespace Zeus.Api.Infrastructure.Services.Integrations.Gmail;

public sealed class GmailService : IGmailService
{
    private readonly IGmailSettingsProvider _gmailSettings;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly HttpClient _httpClient;

    public GmailService(IIntegrationsSettingsProvider settingsProvider)
    {
        _gmailSettings = settingsProvider.Gmail;
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };

        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
    }

    public async Task<ErrorOr<GmailUserTokens>> GetTokensFromOauth2Async(string code)
    {
        _httpClient.DefaultRequestHeaders.Authorization = null;

        var requestContent = new FormUrlEncodedContent([
            new KeyValuePair<string, string>("client_id", _gmailSettings.ClientId),
            new KeyValuePair<string, string>("client_secret", _gmailSettings.ClientSecret),
            new KeyValuePair<string, string>("code", code),
            new KeyValuePair<string, string>("grant_type", "authorization_code"),
            new KeyValuePair<string, string>("scope", string.Join(" ", _gmailSettings.Scopes)),
            new KeyValuePair<string, string>("redirect_uri", _gmailSettings.RedirectUrl),
            new KeyValuePair<string, string>("access_type", "offline"),
            new KeyValuePair<string, string>("prompt", "consent")
        ]);

        HttpResponseMessage response = await _httpClient.PostAsync(_gmailSettings.TokenEndpoint, requestContent);

        if (!response.IsSuccessStatusCode)
            return Errors.Integrations.Gmail.ErrorDuringRequest;

        var responseContent =
            await response.Content.ReadFromJsonAsync<GmailTokensResponse>(_jsonSerializerOptions);
        if (responseContent is null)
            return Errors.Integrations.Gmail.InvalidBody;

        return new GmailUserTokens(
            new AccessToken(responseContent.AccessToken),
            new RefreshToken(responseContent.RefreshToken),
            responseContent.TokenType,
            responseContent.ExpiresIn);
    }

    public async Task<ErrorOr<GmailUser>> GetUserAsync(AccessToken accessToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = GetAuthHeaderBearerValue(accessToken);
        HttpResponseMessage response = await _httpClient.GetAsync(_gmailSettings.UserInfoEndpoint);

        if (!response.IsSuccessStatusCode)
        {
            return Errors.Integrations.Gmail.ErrorDuringRequest;
        }
        var gmailUser = await response.Content.ReadFromJsonAsync<GetGmailUserResponse>(_jsonSerializerOptions);

        if (gmailUser is null)
        {
            return Errors.Integrations.Gmail.InvalidBody;
        }
        return new GmailUser(
            new GmailUserId(gmailUser.Id),
            gmailUser.GivenName,
            gmailUser.FamilyName,
            gmailUser.Name,
            gmailUser.Email,
            new Uri(gmailUser.Picture)
        );
    }

    private static AuthenticationHeaderValue GetAuthHeaderBearerValue(AccessToken accessToken) => new("Bearer", accessToken.Value);
}
