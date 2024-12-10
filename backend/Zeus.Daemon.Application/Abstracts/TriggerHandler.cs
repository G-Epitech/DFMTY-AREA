using Zeus.Daemon.Application.Interfaces;
using Zeus.Daemon.Domain.Automation;
using Zeus.Daemon.Domain.Automation.AutomationAggregate.Entities;

namespace Zeus.Daemon.Application.Abstracts;

public abstract class TriggerHandler : ITriggerHandler
{
    public abstract Task HandleAsync(AutomationExecutionContext context, CancellationToken cancellationToken);
    
    public Task ExecuteAsync(AutomationExecutionContext context, Dictionary<string, string> facts,
        CancellationToken cancellationToken)
    {
        var actions = context.Automation.Actions;

        foreach (AutomationAction action in actions)
        {
            if (action.Providers.Count <= 0)
            {
                continue;
            }

            var integration = context.Integrations.FirstOrDefault(i => i.Id == action.Providers[0]);

            // Execute the action with the integration and facts
        }

        return Task.CompletedTask;
    }
}
