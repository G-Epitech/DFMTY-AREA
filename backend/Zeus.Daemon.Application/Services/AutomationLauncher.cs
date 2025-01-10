using Microsoft.Extensions.DependencyInjection;

using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Daemon.Application.Interfaces;
using Zeus.Daemon.Domain.Automations;

namespace Zeus.Daemon.Application.Services;

public sealed class AutomationLauncher: IAutomationsLauncher
{
    private IAutomationsRunner? _automationsRunner;
    private readonly IServiceProvider _serviceProvider;
    private IAutomationsRunner AutomationsRunner => _automationsRunner ??= _serviceProvider.GetRequiredService<IAutomationsRunner>();

    public AutomationLauncher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task<bool> LaunchAutomationAsync(AutomationId automationId, FactsDictionary facts)
    {
        return AutomationsRunner.RunAsync(automationId, facts);
    }
}
