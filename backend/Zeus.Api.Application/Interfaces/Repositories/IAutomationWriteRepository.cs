using Zeus.Api.Domain.AutomationAggregate;

namespace Zeus.Api.Application.Interfaces.Repositories;

public interface IAutomationWriteRepository
{
    public Task AddAutomationAsync(Automation automation, CancellationToken cancellationToken = default);
    public Task UpdateAutomationAsync(Automation automation, CancellationToken cancellationToken = default);
    public Task DeleteAutomationAsync(Automation automation, CancellationToken cancellationToken = default);
}
