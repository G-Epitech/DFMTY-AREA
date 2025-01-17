using System.Globalization;

using Microsoft.Extensions.Logging;

using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Domain.Integrations.IntegrationAggregate;
using Zeus.Daemon.Application.Attributes;
using Zeus.Daemon.Application.Interfaces;
using Zeus.Daemon.Application.Providers.LeagueOfLegends.Services;
using Zeus.Daemon.Domain.Automations;
using Zeus.Daemon.Domain.LeagueOfLegends;
using Zeus.Daemon.Domain.LeagueOfLegends.ValueObjects;

namespace Zeus.Daemon.Application.Providers.LeagueOfLegends.TriggerHandlers;

[TriggerHandler("LeagueOfLegends.MatchFinishedWithKdaLowerThan")]
public class LeagueOfLegendsMatchFinishedWithKdaLowerThanTrigger
{
    private readonly IAutomationsLauncher _automationsLauncher;
    private readonly ILogger _logger;
    private readonly Dictionary<RiotAccountId, Dictionary<AutomationId, float>> _triggers = new();
    private readonly ILeagueOfLegendsPollingService _pollingService;

    public LeagueOfLegendsMatchFinishedWithKdaLowerThanTrigger(
        IAutomationsLauncher automationsLauncher,
        ILogger<LeagueOfLegendsMatchFinishedWithKdaLowerThanTrigger> logger,
        ILeagueOfLegendsPollingService pollingService)
    {
        _automationsLauncher = automationsLauncher;
        _logger = logger;
        _pollingService = pollingService;

        _pollingService.RegisterNewMatchDetected(OnMatchFinished);
    }

    [OnTriggerRegister]
    public async Task<bool> OnRegisterAsync(
        AutomationId automationId,
        [FromIntegrations] IList<LeagueOfLegendsIntegration> integrations,
        [FromParameters] float kdaThreshold,
        CancellationToken cancellationToken = default)
    {
        foreach (var integration in integrations)
        {
            var accountId = new RiotAccountId(integration.ClientId);

            if (!_triggers.ContainsKey(accountId))
            {
                _triggers[accountId] = [];

                await _pollingService.RegisterRiotAccount(accountId, cancellationToken);
            }

            _triggers[accountId].Add(automationId, kdaThreshold);
        }

        return true;
    }

    [OnTriggerRemove]
    public Task<bool> OnRemoveAsync(AutomationId automationId, CancellationToken cancellationToken = default)
    {
        foreach (var accountId in _triggers.Keys)
        {
            _triggers[accountId].Remove(automationId);

            if (_triggers[accountId].Count != 0)
            {
                continue;
            }

            _triggers.Remove(accountId);
            _ = _pollingService.UnregisterRiotAccount(accountId, cancellationToken);
        }

        return Task.FromResult(true);
    }

    private async Task OnMatchFinished(RiotAccountId accountId, LeagueOfLegendsMatch match,
        CancellationToken cancellationToken)
    {
        if (!_triggers.TryGetValue(accountId, out var automations))
        {
            _logger.LogWarning("No triggers found for account {accountId}", accountId.Value);
            return;
        }

        var participant = match.Participants.FirstOrDefault(p => p.Id == accountId);
        if (participant is null)
        {
            _logger.LogWarning("No participant found for account {accountId} in match {matchId}", accountId.Value,
                match.Id.Value);
            return;
        }

        var facts = new FactsDictionary
        {
            { "MatchId", Fact.Create(match.Id.Value) },
            { "MatchType", Fact.Create(match.MatchType) },
            { "MatchDuration", Fact.Create(match.MatchDuration) },
            { "MatchWon", Fact.Create(participant.Win) },
            { "SummonerKda", Fact.Create(participant.Kda.ToString(CultureInfo.InvariantCulture)) },
            { "SummonerChampion", Fact.Create(participant.ChampionName) }
        };

        foreach (var automation in automations)
        {
            var kda = automation.Value;

            if (participant.Kda < kda)
            {
                await LaunchTargetedAutomation(automation.Key, facts);
            }
        }
    }

    private async Task LaunchTargetedAutomation(AutomationId automationId, FactsDictionary facts
    )
    {
        var res = await _automationsLauncher.LaunchAsync(automationId, facts);

        if (!res)
        {
            _logger.LogWarning("Failed to launch automation {automationId}", automationId.Value);
        }
    }
}
