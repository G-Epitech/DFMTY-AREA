using Zeus.Common.Domain.AutomationAggregate.Entities;
using Zeus.Common.Domain.Integrations.IntegrationAggregate;

namespace Zeus.Daemon.Application.Interfaces;

public interface IActionHandler
{
    public Task HandleAsync(AutomationAction action, IReadOnlyCollection<Integration> integrations, Dictionary<string, string> facts,
        CancellationToken cancellationToken);
}
