using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Domain.Integrations.IntegrationAggregate;
using Zeus.Api.Domain.Integrations.IntegrationLinkRequestAggregate;

namespace Zeus.Api.Infrastructure.Persistence.Repositories;

public sealed class IntegrationWriteRepository: IIntegrationWriteRepository
{
    public Task AddIntegrationAsync(Integration integration)
    {
        InMemoryStore.Integrations.Add(integration);

        return Task.CompletedTask;
    }

    public Task UpdateIntegrationAsync(Integration integration)
    {
        InMemoryStore.Integrations.Remove(integration);
        InMemoryStore.Integrations.Add(integration);

        return Task.CompletedTask;
    }

    public Task DeleteIntegrationAsync(Integration integration)
    {
        InMemoryStore.Integrations.Remove(integration);

        return Task.CompletedTask;
    }
    
    public Task AddIntegrationLinkRequestAsync(IntegrationLinkRequest request)
    {
        InMemoryStore.IntegrationLinkRequests.Add(request);

        return Task.CompletedTask;
    }
    
    public Task UpdateIntegrationLinkRequestAsync(IntegrationLinkRequest request)
    {
        InMemoryStore.IntegrationLinkRequests.Remove(request);
        InMemoryStore.IntegrationLinkRequests.Add(request);

        return Task.CompletedTask;
    }
    
    public Task DeleteIntegrationLinkRequestAsync(IntegrationLinkRequest request)
    {
        InMemoryStore.IntegrationLinkRequests.Remove(request);

        return Task.CompletedTask;
    }
}
