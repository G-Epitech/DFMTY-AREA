using Zeus.Api.Domain.AutomationAggregate;
using Zeus.Api.Domain.AutomationAggregate.ValueObjects;
using Zeus.Api.Domain.UserAggregate.ValueObjects;
using Zeus.Common.Extensions.Queryable;

namespace Zeus.Api.Application.Interfaces.Repositories;

public interface IAutomationReadRepository
{
    public Task<Automation?> GetByIdAsync(AutomationId id, CancellationToken cancellationToken = default);
    public Task<Page<Automation>> GetAutomationsByOwnerIdAsync(UserId ownerId, PageQuery query, CancellationToken cancellationToken = default);
    public Task<Page<Automation>> GetAutomationsAsync(PageQuery query, CancellationToken cancellationToken = default);
}
