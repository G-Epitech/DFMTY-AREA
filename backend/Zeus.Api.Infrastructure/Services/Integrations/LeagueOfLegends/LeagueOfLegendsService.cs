using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

using ErrorOr;

using Zeus.Api.Application.Interfaces.Services.Integrations;
using Zeus.Api.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Api.Domain.Errors.Integrations;
using Zeus.Api.Domain.Integrations.LeagueOfLegends;
using Zeus.Api.Domain.Integrations.LeagueOfLegends.ValueObjects;
using Zeus.Api.Infrastructure.Services.Integrations.LeagueOfLegends.Contracts;

namespace Zeus.Api.Infrastructure.Services.Integrations.LeagueOfLegends;

public class LeagueOfLegendsService : ILeagueOfLegendsService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly Uri _platformApiEndpoint;
    private readonly Uri _regionalApiEndpoint;

    public LeagueOfLegendsService(IIntegrationsSettingsProvider integrationsSettingsProvider)
    {
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };

        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("X-Riot-Token", integrationsSettingsProvider.Riot.ApiKey);
        _httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
        _platformApiEndpoint = new Uri(integrationsSettingsProvider.Riot.PlatformApiEndpoint);
        _regionalApiEndpoint = new Uri(integrationsSettingsProvider.Riot.RegionalApiEndpoint);
    }

    public async Task<ErrorOr<LeagueOfLegendsAccount>> GetAccountByRiotIdAsync(string gameName, string tagLine)
    {
        var requestUri = new Uri(_platformApiEndpoint, $"riot/account/v1/accounts/by-riot-id/{gameName}/{tagLine}");

        HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
        if (!response.IsSuccessStatusCode)
        {
            return Errors.Integrations.LeagueOfLegends.ErrorDuringAccountRequest;
        }

        var responseContent =
            await response.Content.ReadFromJsonAsync<GetLeagueOfLegendsAccountRequest>(_jsonSerializerOptions);
        if (responseContent is null)
        {
            return Errors.Integrations.LeagueOfLegends.InvalidBody;
        }

        return new LeagueOfLegendsAccount(
            new LeagueOfLegendsAccountId(responseContent.Puuid),
            responseContent.GameName,
            responseContent.TagLine);
    }
}
