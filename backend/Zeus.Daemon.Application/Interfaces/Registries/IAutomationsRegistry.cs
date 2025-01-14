using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Extensions.DependencyInjection;
using Zeus.Daemon.Domain.Automations;

namespace Zeus.Daemon.Application.Interfaces.Registries;

[AutoStarted]
public interface IAutomationsRegistry
{
    public Task<bool> RegisterAsync(RegistrableAutomation registrable, CancellationToken cancellationToken = default);
    public Task<bool> RemoveAsync(AutomationId automationId, CancellationToken cancellationToken = default);
    public Automation? GetAutomation(AutomationId automationId);
    public IReadOnlyList<Automation> GetAutomations(IReadOnlyList<AutomationId> automationIds);
}
