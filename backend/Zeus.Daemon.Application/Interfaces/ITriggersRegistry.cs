using Zeus.Common.Domain.AutomationAggregate.Entities;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;

namespace Zeus.Daemon.Application.Interfaces;

public interface ITriggersRegistry
{
    public Task<bool> RegisterAsync(AutomationTrigger trigger, CancellationToken cancellationToken = default);
    public Task<bool> RemoveAsync(AutomationTriggerId triggerId, CancellationToken cancellationToken = default);
}
