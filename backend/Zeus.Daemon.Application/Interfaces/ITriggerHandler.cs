using Zeus.Daemon.Domain.Automations;

namespace Zeus.Daemon.Application.Interfaces;

public interface ITriggerHandler
{
    public Task HandleAsync(AutomationExecutionContext context, CancellationToken cancellationToken);

    public Task CancelAsync(CancellationToken cancellationToken);
    
    protected Task ExecuteAsync(AutomationExecutionContext context, Dictionary<string, string> facts,
        CancellationToken cancellationToken);
}
