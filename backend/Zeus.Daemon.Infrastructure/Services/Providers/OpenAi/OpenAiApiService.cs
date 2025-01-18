using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;

using ErrorOr;

using Zeus.Daemon.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Daemon.Application.Providers.OpenAi.Services;
using Zeus.Daemon.Domain.Errors.Services;
using Zeus.Daemon.Infrastructure.Services.Providers.OpenAi.Contracts;

namespace Zeus.Daemon.Infrastructure.Services.Providers.OpenAi;

public class OpenAiApiService : IOpenAiApiService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public OpenAiApiService(IIntegrationsSettingsProvider settings)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(settings.OpenAi.ApiEndpoint);
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };
    }

    public async Task<ErrorOr<string>> GetCompletionAsync(string context, string prompt, string model, string apiKey,
        CancellationToken cancellationToken = default)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        var requestBody = new JsonObject
        {
            ["model"] = model,
            ["messages"] = new JsonArray
            {
                new JsonObject { ["role"] = "developer", ["content"] = context },
                new JsonObject { ["role"] = "user", ["content"] = prompt },
            }
        };

        var response =
            await _httpClient.PostAsJsonAsync("chat/completions", requestBody, cancellationToken: cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return Errors.Services.OpenAi.FailureDuringRequest;
        }

        var responseContent =
            await response.Content.ReadFromJsonAsync<GetOpenAiCompletionsResult>(_jsonSerializerOptions,
                cancellationToken: cancellationToken);
        if (responseContent is null)
        {
            return Errors.Services.OpenAi.InvalidBody;
        }

        return responseContent.Choices[0].Message.Content;
    }
}
