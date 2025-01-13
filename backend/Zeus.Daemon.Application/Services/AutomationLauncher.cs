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

    public Task<bool> LaunchAutomationAsync(AutomationId automationId, FactsDictionary facts)
    {
        return AutomationsRunner.RunAsync(automationId, facts);
    }
}
