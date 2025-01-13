using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

using ErrorOr;

using Zeus.Api.Application.Interfaces.Services.Integrations;
using Zeus.Api.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Api.Domain.Errors.Integrations;
using Zeus.Api.Domain.Integrations.OpenAi;
using Zeus.Api.Domain.Integrations.OpenAi.ValueObjects;
using Zeus.Api.Infrastructure.Services.Integrations.OpenAi.Contracts;
using Zeus.Common.Domain.Authentication.Common;

namespace Zeus.Api.Infrastructure.Services.Integrations.OpenAi;

public class OpenAiService : IOpenAiService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public OpenAiService(IIntegrationsSettingsProvider integrationsSettingsProvider)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(integrationsSettingsProvider.OpenAi.ApiEndpoint);

        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };
    }

    private AuthenticationHeaderValue GetAuthHeaderBearerValue(AccessToken accessToken) =>
        new AuthenticationHeaderValue("Bearer", accessToken.Value);

    public async Task<ErrorOr<List<OpenAiModel>>> GetModelsAsync(AccessToken accessToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = GetAuthHeaderBearerValue(accessToken);

        HttpResponseMessage response = await _httpClient.GetAsync("models");
        if (!response.IsSuccessStatusCode)
        {
            return Errors.Integrations.OpenAi.ErrorDuringModelsRequest;
        }

        var responseContent =
            await response.Content.ReadFromJsonAsync<GetOpenAiModels>(_jsonSerializerOptions);
        if (responseContent is null)
        {
            return Errors.Integrations.OpenAi.InvalidBody;
        }

        return responseContent.Data.Select(model => new OpenAiModel(
            new OpenAiModelId(model.Id),
            model.Object,
            DateTimeOffset.FromUnixTimeSeconds(model.Created).DateTime,
            model.OwnedBy)).ToList();
    }
}
