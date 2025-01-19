using System.Net.Http.Json;
using System.Text.Json;

using ErrorOr;

using Zeus.Daemon.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Daemon.Application.Providers.LeagueOfLegends.Services;
using Zeus.Daemon.Domain.Errors.Services;
using Zeus.Daemon.Domain.Providers.LeagueOfLegends;
using Zeus.Daemon.Domain.Providers.LeagueOfLegends.ValueObjects;
using Zeus.Daemon.Infrastructure.Services.Providers.LeagueOfLegends.Contracts;

namespace Zeus.Daemon.Infrastructure.Services.Providers.LeagueOfLegends;

public class LeagueOfLegendsApiService : ILeagueOfLegendsApiService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly Uri _platformApiEndpoint;
    private readonly Uri _regionalApiEndpoint;

    public LeagueOfLegendsApiService(IIntegrationsSettingsProvider integrationsSettingsProvider)
    {
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("X-Riot-Token", integrationsSettingsProvider.Riot.ApiKey);

        _platformApiEndpoint = new Uri(integrationsSettingsProvider.Riot.PlatformApiEndpoint);
        _regionalApiEndpoint = new Uri(integrationsSettingsProvider.Riot.RegionalApiEndpoint);
    }

    public async Task<ErrorOr<LeagueOfLegendsMatchId>> GetLastMatchAsync(RiotAccountId accountId,
        CancellationToken cancellationToken = default)
    {
        var requestUri = new Uri(_regionalApiEndpoint,
            $"lol/match/v5/matches/by-puuid/{accountId.Value}/ids?start=0&count=1");

        HttpResponseMessage response = await _httpClient.GetAsync(requestUri, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return Errors.Services.LeagueOfLegends.FailureDuringRequest;
        }

        var responseContent =
            await response.Content.ReadFromJsonAsync<List<string>>(_jsonSerializerOptions, cancellationToken);
        if (responseContent is null)
        {
            return Errors.Services.LeagueOfLegends.InvalidBody;
        }

        if (responseContent.Count == 0)
        {
            return Errors.Services.LeagueOfLegends.MatchNotFound;
        }

        return new LeagueOfLegendsMatchId(responseContent.First());
    }

    public async Task<ErrorOr<LeagueOfLegendsMatch>> GetMatchByIdAsync(LeagueOfLegendsMatchId matchId,
        CancellationToken cancellationToken = default)
    {
        var requestUri = new Uri(_regionalApiEndpoint,
            $"lol/match/v5/matches/{matchId.Value}");

        HttpResponseMessage response = await _httpClient.GetAsync(requestUri, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return Errors.Services.LeagueOfLegends.FailureDuringRequest;
        }

        var responseContent =
            await response.Content.ReadFromJsonAsync<GetLeagueOfLegendsMatchResult>(_jsonSerializerOptions,
                cancellationToken);
        if (responseContent is null)
        {
            return Errors.Services.LeagueOfLegends.InvalidBody;
        }

        var participants = responseContent.Info.Participants.Select(p => new LeagueOfLegendsMatchParticipant(
            new RiotAccountId(p.Puuid),
            p.Win,
            p.ChampionName,
            p.Challenges.Kda
        )).ToList();

        return new LeagueOfLegendsMatch(
            new LeagueOfLegendsMatchId(responseContent.Metadata.MatchId),
            responseContent.Info.GameType,
            responseContent.Info.GameDuration,
            participants);
    }
}
