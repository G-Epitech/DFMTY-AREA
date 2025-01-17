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

[TriggerHandler("LeagueOfLegends.MatchFinished")]
public class LeagueOfLegendsMatchFinishedTrigger
{
    private readonly IAutomationsLauncher _automationsLauncher;
    private readonly ILogger _logger;
    private readonly Dictionary<RiotAccountId, List<AutomationId>> _triggers = new();
    private readonly ILeagueOfLegendsPollingService _pollingService;

    public LeagueOfLegendsMatchFinishedTrigger(
        IAutomationsLauncher automationsLauncher,
        ILogger<LeagueOfLegendsMatchFinishedTrigger> logger, ILeagueOfLegendsPollingService pollingService)
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

            if (!_triggers[accountId].Contains(automationId))
            {
                _triggers[accountId].Add(automationId);
            }
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
        if (!_triggers.TryGetValue(accountId, out List<AutomationId>? automationIds))
        {
            return;
        }

        var participant = match.Participants.FirstOrDefault(p => p.Id == accountId);
        if (participant is null)
        {
            return;
        }

        var facts = new FactsDictionary
        {
            { "MatchId", Fact.Create(match.Id.Value) },
            { "MatchType", Fact.Create(match.MatchType) },
            { "MatchDuration", Fact.Create(match.MatchDuration) },
            { "MatchWon", Fact.Create(participant.Win) },
            { "SummonerKda", Fact.Create(participant.Kda) },
            { "SummonerChampion", Fact.Create(participant.ChampionName) }
        };

        await LaunchTargetedAutomations(automationIds, facts);
    }

    private async Task LaunchTargetedAutomations(List<AutomationId> automationIds, FactsDictionary facts
    )
    {
        var res = await _automationsLauncher.LaunchManyAsync(automationIds, facts);

        foreach ((AutomationId automationId, bool started) in res)
        {
            if (!started)
            {
                _logger.LogError("Automation {id} failed to launch", automationId.Value);
            }
        }
    }
}
