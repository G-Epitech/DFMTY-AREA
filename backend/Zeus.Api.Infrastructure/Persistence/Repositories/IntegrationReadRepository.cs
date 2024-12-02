using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Domain.Integrations.IntegrationAggregate;
using Zeus.Api.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Api.Domain.UserAggregate.ValueObjects;
using Zeus.Common.Extensions.Queryable;

namespace Zeus.Api.Infrastructure.Persistence.Repositories;

public sealed class IntegrationReadRepository : IIntegrationReadRepository
{
    public Task<Integration?> GetIntegrationByIdAsync(IntegrationId id)
    {
        return Task.FromResult(InMemoryStore.Integrations.FirstOrDefault(integration => integration.Id == id));
    }

    public Task<Page<Integration>> GetIntegrationsAsync(PageQuery query)
    {
        var page = InMemoryStore.Integrations
            .AsQueryable()
            .Paginate(query);

        return Task.FromResult(page);
    }

    public Task<Page<Integration>> GetIntegrationsByOwnerIdAsync(UserId userId, PageQuery query)
    {
        var page = InMemoryStore.Integrations
            .AsQueryable()
            .Where(integration => integration.OwnerId == userId)
            .Paginate(query);
        
        return Task.FromResult(page);
    }
}
