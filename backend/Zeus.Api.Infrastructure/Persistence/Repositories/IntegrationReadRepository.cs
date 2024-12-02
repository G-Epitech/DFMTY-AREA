using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Domain.IntegrationAggregate;
using Zeus.Api.Domain.IntegrationAggregate.ValueObjects;

namespace Zeus.Api.Infrastructure.Persistence.Repositories;

public sealed class IntegrationReadRepository: IIntegrationReadRepository
{
    public Task<Integration?> GetIntegrationByIdAsync(IntegrationId id)
    {
        return Task.FromResult(InMemoryStore.Integrations.FirstOrDefault(integration => integration.Id == id));
    }
}
