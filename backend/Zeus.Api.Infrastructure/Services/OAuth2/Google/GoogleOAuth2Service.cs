using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

using ErrorOr;

using Zeus.Api.Application.Interfaces.Services.OAuth2;
using Zeus.Api.Application.Interfaces.Services.Settings.OAuth2;
using Zeus.Api.Domain.Errors.OAuth2;
using Zeus.Api.Domain.OAuth2.Google;
using Zeus.Api.Domain.OAuth2.Google.ValueObjects;
using Zeus.Api.Infrastructure.Services.OAuth2.Google.Contracts;
using Zeus.Common.Domain.Authentication.Common;

namespace Zeus.Api.Infrastructure.Services.OAuth2.Google;

public class GoogleOAuth2Service : IGoogleOAuth2Service
{
    private readonly HttpClient _httpClient;
    private readonly IOAuth2GoogleSettingsProvider _googleSettings;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public GoogleOAuth2Service(IOAuth2SettingsProvider settingsProvider)
    {
        _googleSettings = settingsProvider.Google;
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };

        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
    }
    
    private static AuthenticationHeaderValue GetAuthHeaderBearerValue(AccessToken accessToken) =>
        new AuthenticationHeaderValue(
            "Bearer",
            accessToken.Value);

    public async Task<ErrorOr<GoogleUserTokens>> GetTokensFromOauth2Async(string code)
    {
        _httpClient.DefaultRequestHeaders.Authorization = null;
        
        var requestContent = new FormUrlEncodedContent([
            new KeyValuePair<string, string>("client_id", _googleSettings.ClientId),
            new KeyValuePair<string, string>("client_secret", _googleSettings.ClientSecret),
            new KeyValuePair<string, string>("code", code),
            new KeyValuePair<string, string>("grant_type", "authorization_code"),
            new KeyValuePair<string, string>("redirect_uri", _googleSettings.RedirectUrl)
        ]);

        HttpResponseMessage response = await _httpClient.PostAsync(_googleSettings.TokenEndpoint, requestContent);

        if (!response.IsSuccessStatusCode)
            return Errors.OAuth2.Google.ErrorDuringTokenRequest;

        var responseContent =
            await response.Content.ReadFromJsonAsync<GoogleOauth2TokenResponse>(_jsonSerializerOptions);
        if (responseContent is null)
            return Errors.OAuth2.Google.InvalidBody;

        return new GoogleUserTokens(
            new AccessToken(responseContent.AccessToken),
            new RefreshToken(responseContent.RefreshToken),
            responseContent.TokenType,
            responseContent.ExpiresIn);
    }

    public async Task<ErrorOr<GoogleUser>> GetUserAsync(AccessToken accessToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = GetAuthHeaderBearerValue(accessToken);
        
        HttpResponseMessage response = await _httpClient.GetAsync($"{_googleSettings.ApiEndpoint}/oauth2/v2/userinfo");
        
        if (!response.IsSuccessStatusCode)
            return Errors.OAuth2.Google.ErrorDuringUserRequest;
        
        var responseContent = await response.Content.ReadFromJsonAsync<GetGoogleUserResponse>(_jsonSerializerOptions);
        if (responseContent is null)
            return Errors.OAuth2.Google.InvalidBody;

        return GoogleUser.Create(
            new GoogleUserId(responseContent.Id),
            responseContent.Email,
            responseContent.Name,
            responseContent.GivenName,
            responseContent.FamilyName,
            new Uri(responseContent.Picture));
    }
}
