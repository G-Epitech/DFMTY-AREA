using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Daemon.Application.Execution;
using Zeus.Daemon.Application.Interfaces;
using Zeus.Daemon.Application.Interfaces.HandlerProviders;
using Zeus.Daemon.Application.Interfaces.Registries;
using Zeus.Daemon.Domain.Automations;

namespace Zeus.Daemon.Application.Services;

public sealed class AutomationsRunner : IAutomationsRunner
{
    private readonly IAutomationsRegistry _automationsRegistry;
    private readonly IActionHandlersProvider _actionHandlersProvider;
    private readonly IServiceProvider _serviceProvider;
    private readonly List<AutomationExecutionContext> _executions = [];

    public AutomationsRunner(IAutomationsRegistry automationsRegistry, IServiceProvider serviceProvider, IActionHandlersProvider actionHandlersProvider)
    {
        _automationsRegistry = automationsRegistry;
        _serviceProvider = serviceProvider;
        _actionHandlersProvider = actionHandlersProvider;
    }

    public Task<bool> RunAsync(AutomationId automationId, FactsDictionary facts)
    {
        var automation = _automationsRegistry.GetAutomation(automationId);
        if (automation is null)
        {
            Console.WriteLine($"Automation {automationId.Value.ToString()} not found");
            return Task.FromResult(false);
        }

        var ctx = new AutomationExecutionContext(_actionHandlersProvider, automation, [], new FactsDictionary());
        
        _executions.Add(ctx);
        ctx.Run();
        Console.WriteLine($"Running automation {automation.Id.Value.ToString()} that have {automation.Actions.Count} actions");
        return Task.FromResult(true);
    }
}
