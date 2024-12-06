using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Domain.Integrations.IntegrationAggregate;
using Zeus.Api.Domain.Integrations.IntegrationLinkRequestAggregate;

namespace Zeus.Api.Infrastructure.Persistence.Repositories;

public sealed class IntegrationWriteRepository: IIntegrationWriteRepository
{
    private readonly ZeusDbContext _dbContext;

    public IntegrationWriteRepository(ZeusDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddIntegrationAsync(Integration integration)
    {
        _dbContext.Integrations.Add(integration);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateIntegrationAsync(Integration integration)
    {
        _dbContext.Integrations.Update(integration);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteIntegrationAsync(Integration integration)
    {
        _dbContext.Integrations.Remove(integration);
        await _dbContext.SaveChangesAsync();
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
