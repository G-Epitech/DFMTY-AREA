using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Extensions.DependencyInjection;
using Zeus.Daemon.Domain.Automations;

namespace Zeus.Daemon.Application.Interfaces;

[AutoStarted]
public interface IAutomationsLauncher
{
    public Task<bool> LaunchAsync(AutomationId automationId, FactsDictionary facts);
    public Task<Dictionary<AutomationId, bool>> LaunchManyAsync(IReadOnlyList<AutomationId> automationIds, FactsDictionary facts);
}
