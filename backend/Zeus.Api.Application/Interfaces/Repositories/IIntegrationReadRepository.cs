using Zeus.Api.Domain.Integrations.IntegrationAggregate;
using Zeus.Api.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Api.Domain.UserAggregate.ValueObjects;
using Zeus.Common.Extensions.Queryable;

namespace Zeus.Api.Application.Interfaces.Repositories;

public interface IIntegrationReadRepository
{
    public Task<Integration?> GetIntegrationByIdAsync(IntegrationId id,
        CancellationToken cancellationToken = default);
    public Task<Page<Integration>> GetIntegrationsAsync(PageQuery query,
        CancellationToken cancellationToken = default);
    public Task<Page<Integration>> GetIntegrationsByOwnerIdAsync(UserId userId, PageQuery query,
        CancellationToken cancellationToken = default);
}
