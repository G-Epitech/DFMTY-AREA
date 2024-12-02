using Zeus.Api.Domain.Integrations.IntegrationAggregate;
using Zeus.Api.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Api.Domain.UserAggregate.ValueObjects;
using Zeus.Common.Extensions.Queryable;

namespace Zeus.Api.Application.Interfaces.Repositories;

public interface IIntegrationReadRepository
{
    public Task<Integration?> GetIntegrationByIdAsync(IntegrationId id);
    public Task<Page<Integration>> GetIntegrationsAsync(PageQuery query);
    public Task<Page<Integration>> GetIntegrationsByOwnerIdAsync(UserId userId, PageQuery query);
}
