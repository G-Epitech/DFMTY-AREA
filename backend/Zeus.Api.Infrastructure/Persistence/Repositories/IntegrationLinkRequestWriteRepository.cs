using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Domain.Integrations.IntegrationLinkRequestAggregate;

namespace Zeus.Api.Infrastructure.Persistence.Repositories;

public sealed class IntegrationLinkRequestWriteRepository : IIntegrationLinkRequestWriteRepository
{
    private readonly ZeusDbContext _dbContext;

    public IntegrationLinkRequestWriteRepository(ZeusDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddRequestAsync(IntegrationLinkRequest request, CancellationToken cancellationToken = default)
    {
        _dbContext.IntegrationLinkRequests.Add(request);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateRequestAsync(IntegrationLinkRequest request, CancellationToken cancellationToken = default)
    {
        _dbContext.IntegrationLinkRequests.Update(request);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteRequestAsync(IntegrationLinkRequest request, CancellationToken cancellationToken = default)
    {
        _dbContext.IntegrationLinkRequests.Remove(request);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
