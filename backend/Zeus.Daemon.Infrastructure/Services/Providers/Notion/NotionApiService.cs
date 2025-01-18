using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

using ErrorOr;

using MapsterMapper;

using Zeus.Common.Domain.Authentication.Common;
using Zeus.Daemon.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Daemon.Application.Providers.Notion.Services;
using Zeus.Daemon.Domain.Errors.Services;
using Zeus.Daemon.Domain.Providers.Notion;
using Zeus.Daemon.Infrastructure.Services.Providers.Notion.Contracts;

namespace Zeus.Daemon.Infrastructure.Services.Providers.Notion;

public class NotionApiService : INotionApiService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly IMapper _mapper;

    public NotionApiService(IIntegrationsSettingsProvider integrationsSettingsProvider, IMapper mapper)
    {
        _mapper = mapper;
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };

        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(integrationsSettingsProvider.Notion.ApiEndpoint);
        _httpClient.DefaultRequestHeaders.Add("Notion-Version", "2022-06-28");
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
    }

    private AuthenticationHeaderValue GetAuthHeaderBearerValue(AccessToken accessToken) =>
        new AuthenticationHeaderValue("Bearer", accessToken.Value);

    public async Task<ErrorOr<List<NotionDatabase>>> GetWorkspaceDatabasesAsync(AccessToken accessToken,
        CancellationToken cancellationToken = default)
    {
        _httpClient.DefaultRequestHeaders.Authorization = GetAuthHeaderBearerValue(accessToken);

        var filter = new { property = "object", value = "database" };

        var requestContent = new StringContent(
            JsonSerializer.Serialize(new { filter }),
            Encoding.UTF8,
            "application/json"
        );

        HttpResponseMessage response = await _httpClient.PostAsync("search", requestContent, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return Errors.Services.Notion.ErrorDuringSearchRequest;
        }

        var responseContent =
            await response.Content.ReadFromJsonAsync<SearchNotionDatabasesResponse>(_jsonSerializerOptions,
                cancellationToken: cancellationToken);
        if (responseContent is null)
        {
            return Errors.Services.Notion.InvalidBody;
        }

        return responseContent.Results.Select(database => _mapper.Map<NotionDatabase>(database)).ToList();
    }

    public async Task<ErrorOr<List<NotionPage>>> GetWorkspacePagesAsync(AccessToken accessToken,
        CancellationToken cancellationToken = default)
    {
        _httpClient.DefaultRequestHeaders.Authorization = GetAuthHeaderBearerValue(accessToken);

        var filter = new { property = "object", value = "page" };

        var requestContent = new StringContent(
            JsonSerializer.Serialize(new { filter }),
            Encoding.UTF8,
            "application/json"
        );

        HttpResponseMessage response = await _httpClient.PostAsync("search", requestContent, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return Errors.Services.Notion.ErrorDuringSearchRequest;
        }

        var responseContent =
            await response.Content.ReadFromJsonAsync<SearchNotionPagesResponse>(_jsonSerializerOptions,
                cancellationToken);
        if (responseContent is null)
        {
            return Errors.Services.Notion.InvalidBody;
        }

        return responseContent.Results.Select(database => _mapper.Map<NotionPage>(database)).ToList();
    }
}
