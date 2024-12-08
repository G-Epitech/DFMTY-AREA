using Microsoft.EntityFrameworkCore;

using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Domain.Integrations.IntegrationLinkRequestAggregate;
using Zeus.Api.Domain.Integrations.IntegrationLinkRequestAggregate.ValueObjects;

namespace Zeus.Api.Infrastructure.Persistence.Repositories;

public sealed class IntegrationLinkRequestReadRepository : IIntegrationLinkRequestReadRepository
{
    private readonly ZeusDbContext _dbContext;

    private IAsyncQueryable<IntegrationLinkRequest> IntegrationLinkRequests => _dbContext.IntegrationLinkRequests
        .AsNoTracking()
        .AsAsyncEnumerable()
        .AsAsyncQueryable();

    public IntegrationLinkRequestReadRepository(ZeusDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IntegrationLinkRequest?> GetRequestByIdAsync(IntegrationLinkRequestId id, CancellationToken cancellationToken = default)
    {
        return await IntegrationLinkRequests.FirstOrDefaultAsync(request => request.Id == id, cancellationToken: cancellationToken);
    }
}
