using Microsoft.Extensions.DependencyInjection;

using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Daemon.Application.Interfaces;
using Zeus.Daemon.Domain.Automations;

namespace Zeus.Daemon.Application.Services;

public sealed class AutomationLauncher : IAutomationsLauncher
{
    private readonly IServiceProvider _serviceProvider;
    private IAutomationsRunner? _automationsRunner;

    public AutomationLauncher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    private IAutomationsRunner AutomationsRunner => _automationsRunner ??= _serviceProvider.GetRequiredService<IAutomationsRunner>();

    public Task<bool> LaunchAsync(AutomationId automationId, FactsDictionary facts)
    {
        return AutomationsRunner.RunAsync(automationId, facts);
    }

    public Task<Dictionary<AutomationId, bool>> LaunchManyAsync(IReadOnlyList<AutomationId> automationIds, FactsDictionary facts)
    {
        return automationIds.Count == 0
            ? Task.FromResult(new Dictionary<AutomationId, bool>())
            : AutomationsRunner.RunManyAsync(automationIds, facts);
    }
}
