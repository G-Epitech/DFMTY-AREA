using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Common.Domain.Integrations.IntegrationAggregate;

namespace Zeus.Api.Infrastructure.Persistence.Repositories;

using Integration = Zeus.Common.Domain.Integrations.IntegrationAggregate.Integration;

public sealed class IntegrationWriteRepository : IIntegrationWriteRepository
{
    private readonly ZeusDbContext _dbContext;

    public IntegrationWriteRepository(ZeusDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddIntegrationAsync(Integration integration, CancellationToken cancellationToken = default)
    {
        _dbContext.Integrations.Add(integration);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateIntegrationAsync(Integration integration, CancellationToken cancellationToken = default)
    {
        _dbContext.Integrations.Update(integration);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteIntegrationAsync(Integration integration, CancellationToken cancellationToken = default)
    {
        _dbContext.Integrations.Remove(integration);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
