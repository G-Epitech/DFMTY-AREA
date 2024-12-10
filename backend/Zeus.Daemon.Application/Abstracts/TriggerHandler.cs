using Microsoft.Extensions.DependencyInjection;

using Zeus.Daemon.Application.Interfaces;
using Zeus.Daemon.Application.Mappers;
using Zeus.Daemon.Domain.Automation;
using Zeus.Daemon.Domain.Automation.AutomationAggregate.Entities;
using Zeus.Daemon.Domain.IntegrationAggregate;

namespace Zeus.Daemon.Application.Abstracts;

public abstract class TriggerHandler : ITriggerHandler
{
    private readonly IServiceProvider _serviceProvider;

    protected TriggerHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public abstract Task HandleAsync(AutomationExecutionContext context, CancellationToken cancellationToken);

    public async Task ExecuteAsync(AutomationExecutionContext context, Dictionary<string, string> facts,
        CancellationToken cancellationToken)
    {
        var actions = context.Automation.Actions;

        foreach (AutomationAction action in actions)
        {
            if (_serviceProvider.GetRequiredService(action.GetHandler()) is not IActionHandler handler)
            {
                continue;
            }

            await handler.HandleAsync(action, new List<Integration>(), facts, cancellationToken);
        }
    }
}
