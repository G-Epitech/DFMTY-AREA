using Microsoft.EntityFrameworkCore;

using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Domain.Integrations.IntegrationAggregate;
using Zeus.Api.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Api.Domain.Integrations.IntegrationLinkRequestAggregate;
using Zeus.Api.Domain.Integrations.IntegrationLinkRequestAggregate.ValueObjects;
using Zeus.Api.Domain.UserAggregate.ValueObjects;
using Zeus.Common.Extensions.Queryable;

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

    public async Task<IntegrationLinkRequest?> GetRequestByIdAsync(IntegrationLinkRequestId id)
    {
        return await IntegrationLinkRequests.FirstOrDefaultAsync(request => request.Id == id);
    }
}
