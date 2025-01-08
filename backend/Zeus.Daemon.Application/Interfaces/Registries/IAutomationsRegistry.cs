using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Extensions.DependencyInjection;

namespace Zeus.Daemon.Application.Interfaces.Registries;

[AutoStarted]
public interface IAutomationsRegistry
{
    public Task<bool> RegisterAsync(Automation automation, CancellationToken cancellationToken = default);
    public Task<bool> RemoveAsync(AutomationId automationId, CancellationToken cancellationToken = default);
    public Automation? GetAutomation(AutomationId automationId);
}
