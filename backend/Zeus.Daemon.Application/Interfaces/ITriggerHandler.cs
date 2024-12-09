using Zeus.Daemon.Domain.Automation;

namespace Zeus.Daemon.Application.Interfaces;

public interface ITriggerHandler
{
    public Task HandleAsync(AutomationExecutionContext context, CancellationToken cancellationToken);

    protected Task ListenAsync(AutomationExecutionContext context, CancellationToken cancellationToken);

    protected Task ExecuteAsync(AutomationExecutionContext context, Dictionary<string, string> facts,
        CancellationToken cancellationToken);
}
