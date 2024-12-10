using Zeus.Api.Domain.AutomationAggregate;
using Zeus.Api.Domain.AutomationAggregate.ValueObjects;
using Zeus.Api.Domain.UserAggregate.ValueObjects;
using Zeus.Common.Extensions.Queryable;

namespace Zeus.Api.Application.Interfaces.Repositories;

public interface IAutomationWriteRepository
{
    public Task AddAutomationAsync(Automation automation, CancellationToken cancellationToken = default);
    public Task UpdateAutomationAsync(Automation automation, CancellationToken cancellationToken = default);
    public Task DeleteAutomationAsync(Automation automation, CancellationToken cancellationToken = default);
}
