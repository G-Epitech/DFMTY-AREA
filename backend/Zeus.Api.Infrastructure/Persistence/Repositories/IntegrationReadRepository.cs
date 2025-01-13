using Microsoft.EntityFrameworkCore;

using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Common.Domain.UserAggregate.ValueObjects;
using Zeus.Common.Extensions.Queryable;

namespace Zeus.Api.Infrastructure.Persistence.Repositories;

using Integration = Common.Domain.Integrations.IntegrationAggregate.Integration;

public sealed class IntegrationReadRepository : IIntegrationReadRepository
{
    private readonly ZeusDbContext _dbContext;

    public IntegrationReadRepository(ZeusDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private IAsyncQueryable<Integration> Integrations => _dbContext.Integrations.AsNoTracking()
        .AsAsyncEnumerable()
        .AsAsyncQueryable();

    public async Task<Integration?> GetIntegrationByIdAsync(IntegrationId id, CancellationToken cancellationToken = default)
    {
        return await Integrations.FirstOrDefaultAsync(integration => integration.Id == id, cancellationToken: cancellationToken);
    }

    public async Task<Page<Integration>> GetIntegrationsAsync(PageQuery query, CancellationToken cancellationToken = default)
    {
        return await Integrations.PaginateAsync(query, cancellationToken: cancellationToken);
    }

    public Task<Page<Integration>> GetIntegrationsByOwnerIdAsync(UserId userId, PageQuery query, CancellationToken cancellationToken = default)
    {
        var page = Integrations
            .Where(integration => integration.OwnerId == userId)
            .PaginateAsync(query, cancellationToken: cancellationToken);

        return page;
    }
}
