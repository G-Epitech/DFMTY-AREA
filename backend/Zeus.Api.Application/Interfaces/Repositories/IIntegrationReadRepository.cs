using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Common.Domain.UserAggregate.ValueObjects;
using Zeus.Common.Extensions.Queryable;

namespace Zeus.Api.Application.Interfaces.Repositories;

using Integration = Zeus.Common.Domain.Integrations.IntegrationAggregate.Integration;

public interface IIntegrationReadRepository
{
    public Task<Integration?> GetIntegrationByIdAsync(IntegrationId id,
        CancellationToken cancellationToken = default);

    public Task<Page<Integration>> GetIntegrationsAsync(PageQuery query,
        CancellationToken cancellationToken = default);

    public Task<Page<Integration>> GetIntegrationsByOwnerIdAsync(UserId userId, PageQuery query,
        CancellationToken cancellationToken = default);
}
