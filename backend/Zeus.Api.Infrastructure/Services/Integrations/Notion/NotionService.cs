using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

using ErrorOr;

using MapsterMapper;

using Zeus.Api.Application.Interfaces.Services.Integrations;
using Zeus.Api.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Api.Domain.Errors.Integrations;
using Zeus.Api.Domain.Integrations.Notion;
using Zeus.Api.Domain.Integrations.Notion.ValueObjects;
using Zeus.Api.Infrastructure.Services.Integrations.Notion.Contracts;
using Zeus.Common.Domain.Authentication.Common;

namespace Zeus.Api.Infrastructure.Services.Integrations.Notion;

public class NotionService : INotionService
{
    private readonly HttpClient _httpClient;
    private readonly IIntegrationsSettingsProvider _integrationsSettingsProvider;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly IMapper _mapper;

    public NotionService(IIntegrationsSettingsProvider integrationsSettingsProvider, IMapper mapper)
    {
        _integrationsSettingsProvider = integrationsSettingsProvider;
        _mapper = mapper;
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };

        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(_integrationsSettingsProvider.Notion.ApiEndpoint);
        _httpClient.DefaultRequestHeaders.Add("Notion-Version", "2022-06-28");
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
    }

    private AuthenticationHeaderValue GetAuthHeaderClientValue => new AuthenticationHeaderValue(
        "Basic",
        Convert.ToBase64String(Encoding.UTF8.GetBytes(
            $"{_integrationsSettingsProvider.Notion.ClientId}:{_integrationsSettingsProvider.Notion.ClientSecret}")));

    private AuthenticationHeaderValue GetAuthHeaderBearerValue(AccessToken accessToken) =>
        new AuthenticationHeaderValue("Bearer", accessToken.Value);

    public async Task<ErrorOr<NotionWorkspaceTokens>> GetTokensFromOauth2Async(string code)
    {
        _httpClient.DefaultRequestHeaders.Authorization = GetAuthHeaderClientValue;

        var requestContent = new FormUrlEncodedContent([
            new KeyValuePair<string, string>("grant_type", "authorization_code"),
            new KeyValuePair<string, string>("code", code),
            new KeyValuePair<string, string>("redirect_uri", _integrationsSettingsProvider.Notion.RedirectUrl)
        ]);

        HttpResponseMessage response = await _httpClient.PostAsync("oauth/token", requestContent);

        if (!response.IsSuccessStatusCode)
        {
            return Errors.Integrations.Notion.ErrorDuringTokenRequest;
        }

        var responseContent =
            await response.Content.ReadFromJsonAsync<NotionOAuth2TokenResponse>(_jsonSerializerOptions);
        if (responseContent is null)
            return Errors.Integrations.Notion.InvalidBody;

        return new NotionWorkspaceTokens(
            new AccessToken(responseContent.AccessToken),
            new NotionWorkspaceId(responseContent.WorkspaceId),
            responseContent.BotId);
    }

    public async Task<ErrorOr<NotionBot>> GetBotAsync(AccessToken accessToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = GetAuthHeaderBearerValue(accessToken);

        HttpResponseMessage response = await _httpClient.GetAsync("users/me");

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            return Errors.Integrations.Notion.ErrorDuringBotRequest;
        }

        var responseContent = await response.Content.ReadFromJsonAsync<GetNotionBotResponse>(_jsonSerializerOptions);
        if (responseContent is null)
            return Errors.Integrations.Notion.InvalidBody;

        var avatarUri = responseContent.Bot.Owner.User.AvatarUrl is not null
            ? new Uri(responseContent.Bot.Owner.User.AvatarUrl)
            : null;

        return NotionBot.Create(
            new NotionIntegrationId(responseContent.Id),
            responseContent.Name,
            responseContent.Bot.WorkspaceName,
            NotionUser.Create(
                new NotionUserId(responseContent.Bot.Owner.User.Id),
                responseContent.Bot.Owner.User.Name,
                avatarUri,
                responseContent.Bot.Owner.User.Person.Email));
    }

    public async Task<ErrorOr<List<NotionDatabase>>> GetWorkspaceDatabasesAsync(AccessToken accessToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = GetAuthHeaderBearerValue(accessToken);

        var filter = new { property = "object", value = "database" };

        var requestContent = new StringContent(
            JsonSerializer.Serialize(new { filter }),
            Encoding.UTF8,
            "application/json"
        );

        HttpResponseMessage response = await _httpClient.PostAsync("search", requestContent);

        if (!response.IsSuccessStatusCode)
        {
            return Errors.Integrations.Notion.ErrorDuringSearchRequest;
        }

        var responseContent =
            await response.Content.ReadFromJsonAsync<SearchNotionDatabasesResponse>(_jsonSerializerOptions);
        if (responseContent is null)
        {
            return Errors.Integrations.Notion.InvalidBody;
        }

        return responseContent.Results.Select(database => _mapper.Map<NotionDatabase>(database)).ToList();
    }

    public async Task<ErrorOr<List<NotionPage>>> GetWorkspacePagesAsync(AccessToken accessToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = GetAuthHeaderBearerValue(accessToken);

        var filter = new { property = "object", value = "page" };

        var requestContent = new StringContent(
            JsonSerializer.Serialize(new { filter }),
            Encoding.UTF8,
            "application/json"
        );

        HttpResponseMessage response = await _httpClient.PostAsync("search", requestContent);

        if (!response.IsSuccessStatusCode)
        {
            return Errors.Integrations.Notion.ErrorDuringSearchRequest;
        }

        var responseContent =
            await response.Content.ReadFromJsonAsync<SearchNotionPagesResponse>(_jsonSerializerOptions);
        if (responseContent is null)
        {
            return Errors.Integrations.Notion.InvalidBody;
        }

        return responseContent.Results.Select(database => _mapper.Map<NotionPage>(database)).ToList();
    }
}
