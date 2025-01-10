using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Extensions.DependencyInjection;

namespace Zeus.Daemon.Application.Interfaces.Registries;

[AutoStarted]
public interface ITriggersRegistry
{
    public Task<bool> RegisterAsync(Automation automation, CancellationToken cancellationToken = default);
    public Task<bool> RemoveAsync(Automation automationId, CancellationToken cancellationToken = default);
}
