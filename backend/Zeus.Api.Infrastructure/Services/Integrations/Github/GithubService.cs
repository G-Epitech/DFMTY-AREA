using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Web;

using ErrorOr;

using Zeus.Api.Application.Interfaces.Services.Integrations;
using Zeus.Api.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Api.Domain.Errors.Integrations;
using Zeus.Api.Domain.Integrations.Github;
using Zeus.Api.Infrastructure.Services.Integrations.Github.Contracts;
using Zeus.Common.Domain.Authentication.Common;

namespace Zeus.Api.Infrastructure.Services.Integrations.Github;

public class GithubService : IGithubService
{
    private readonly IIntegrationsSettingsProvider _settingsProvider;
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public GithubService(IIntegrationsSettingsProvider settingsProvider)
    {
        _settingsProvider = settingsProvider;

        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };

        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<ErrorOr<GithubTokens>> GetTokensFromOauth2Async(string code)
    {
        var queryString = HttpUtility.ParseQueryString(String.Empty);
        queryString.Add("client_id", _settingsProvider.Github.ClientId);
        queryString.Add("client_secret", _settingsProvider.Github.ClientSecret);
        queryString.Add("code", code);
        queryString.Add("redirect_uri", _settingsProvider.Github.RedirectUrl);

        var uri = new UriBuilder($"{_settingsProvider.Github.OAuth2Endpoint}login/oauth/access_token")
        {
            Query = queryString.ToString()
        }.Uri;

        HttpResponseMessage response = await _httpClient.PostAsync(uri, null);
        if (!response.IsSuccessStatusCode)
        {
            return Errors.Integrations.Github.ErrorDuringTokenRequest;
        }

        var responseContent = await response.Content.ReadFromJsonAsync<GetGithubTokens>(_jsonSerializerOptions);
        if (responseContent == null)
        {
            return Errors.Integrations.Github.InvalidBody;
        }

        return new GithubTokens(new AccessToken(responseContent.AccessToken), responseContent.TokenType);
    }
}
