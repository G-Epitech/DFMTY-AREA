using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Daemon.Domain.Automations;

namespace Zeus.Daemon.Application.Interfaces;

public interface IAutomationsRegistry
{
    public Task<bool> RegisterAsync(Automation automation, CancellationToken cancellationToken = default);
    public Task<bool> RemoveAsync(AutomationId automationId, CancellationToken cancellationToken = default);
    public Task<bool> RunAsync(AutomationId automationId, IReadOnlyDictionary<string, Fact> facts);
}
