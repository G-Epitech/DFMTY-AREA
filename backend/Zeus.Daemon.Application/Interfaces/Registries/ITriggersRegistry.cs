using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Extensions.DependencyInjection;
using Zeus.Daemon.Domain.Automations;

namespace Zeus.Daemon.Application.Interfaces.Registries;

[AutoStarted]
public interface ITriggersRegistry
{
    public Task<bool> RegisterAsync(RegistrableAutomation registrable, CancellationToken cancellationToken = default);
    public Task<bool> RemoveAsync(Automation automationId, CancellationToken cancellationToken = default);
}
