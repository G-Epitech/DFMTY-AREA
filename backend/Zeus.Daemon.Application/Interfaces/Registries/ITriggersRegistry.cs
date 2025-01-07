using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;

namespace Zeus.Daemon.Application.Interfaces.Registries;

public interface ITriggersRegistry
{
    public Task<bool> RegisterAsync(Automation automation, CancellationToken cancellationToken = default);
    public Task<bool> RemoveAsync(AutomationId automationId, CancellationToken cancellationToken = default);
}
