using Zeus.Daemon.Domain.Automation.AutomationAggregate.Entities;
using Zeus.Daemon.Domain.IntegrationAggregate;

namespace Zeus.Daemon.Application.Interfaces;

public interface IActionHandler
{
    public Task HandleAsync(AutomationAction action, IReadOnlyCollection<Integration> integrations, Dictionary<string, string> facts,
        CancellationToken cancellationToken);
}
