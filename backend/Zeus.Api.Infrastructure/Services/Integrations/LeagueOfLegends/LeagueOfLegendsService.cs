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
    private readonly Uri _dataDragonApiEndpoint;

    public LeagueOfLegendsService(IIntegrationsSettingsProvider integrationsSettingsProvider)
    {
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("X-Riot-Token", integrationsSettingsProvider.Riot.ApiKey);

        _platformApiEndpoint = new Uri(integrationsSettingsProvider.Riot.PlatformApiEndpoint);
        _regionalApiEndpoint = new Uri(integrationsSettingsProvider.Riot.RegionalApiEndpoint);
        _dataDragonApiEndpoint = new Uri(integrationsSettingsProvider.Riot.DataDragonApiEndpoint);
    }

    public async Task<ErrorOr<RiotAccount>> GetRiotAccountByNameAsync(string gameName, string tagLine)
    {
        var requestUri = new Uri(_regionalApiEndpoint, $"riot/account/v1/accounts/by-riot-id/{gameName}/{tagLine}");

        HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
        if (!response.IsSuccessStatusCode)
        {
            return Errors.Integrations.LeagueOfLegends.ErrorDuringAccountRequest;
        }

        var responseContent =
            await response.Content.ReadFromJsonAsync<GetLeagueOfLegendsAccountResponse>(_jsonSerializerOptions);
        if (responseContent is null)
        {
            return Errors.Integrations.LeagueOfLegends.InvalidBody;
        }

        return new RiotAccount(
            new RiotAccountId(responseContent.Puuid),
            responseContent.GameName,
            responseContent.TagLine);
    }

    public async Task<ErrorOr<RiotAccount>> GetRiotAccountByIdAsync(RiotAccountId riotAccountId)
    {
        var requestUri = new Uri(_regionalApiEndpoint, $"riot/account/v1/accounts/by-puuid/{riotAccountId.Value}");

        HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
        if (!response.IsSuccessStatusCode)
        {
            return Errors.Integrations.LeagueOfLegends.ErrorDuringAccountRequest;
        }

        var responseContent =
            await response.Content.ReadFromJsonAsync<GetLeagueOfLegendsAccountResponse>(_jsonSerializerOptions);
        if (responseContent is null)
        {
            return Errors.Integrations.LeagueOfLegends.InvalidBody;
        }

        return new RiotAccount(
            new RiotAccountId(responseContent.Puuid),
            responseContent.GameName,
            responseContent.TagLine);
    }

    public async Task<ErrorOr<LeagueOfLegendsSummoner>> GetSummonerByRiotAccountId(RiotAccountId riotAccountId)
    {
        var requestUri = new Uri(_platformApiEndpoint, $"lol/summoner/v4/summoners/by-puuid/{riotAccountId.Value}");

        HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
        if (!response.IsSuccessStatusCode)
        {
            return Errors.Integrations.LeagueOfLegends.ErrorDuringSummonerRequest;
        }



        var responseContent =
            await response.Content.ReadFromJsonAsync<GetLeagueOfLegendsSummonerResponse>(_jsonSerializerOptions);
        if (responseContent is null)
        {
            return Errors.Integrations.LeagueOfLegends.InvalidBody;
        }

        return new LeagueOfLegendsSummoner(
            new LeagueOfLegendsSummonerId(responseContent.Id),
            new LeagueOfLegendsSummonerAccountId(responseContent.AccountId),
            new RiotAccountId(responseContent.AccountId),
            new LeagueOfLegendsSummonerProfileIconId(responseContent.ProfileIconId),
            responseContent.RevisionDate,
            responseContent.SummonerLevel);
    }

    public Uri GetSummonerProfileIconUri(LeagueOfLegendsSummonerProfileIconId profileIconId)
    {
        return new Uri(_dataDragonApiEndpoint, $"cdn/15.1.1/img/profileicon/{profileIconId.Value}.png");
    }
}
