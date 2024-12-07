using Microsoft.EntityFrameworkCore;

using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Domain.Integrations.IntegrationAggregate;
using Zeus.Api.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Api.Domain.Integrations.IntegrationLinkRequestAggregate;
using Zeus.Api.Domain.Integrations.IntegrationLinkRequestAggregate.ValueObjects;
using Zeus.Api.Domain.UserAggregate.ValueObjects;
using Zeus.Common.Extensions.Queryable;

namespace Zeus.Api.Infrastructure.Persistence.Repositories;

public sealed class IntegrationReadRepository : IIntegrationReadRepository
{
    private readonly ZeusDbContext _dbContext;

    private IAsyncQueryable<Integration> Integrations => _dbContext.Integrations.AsNoTracking()
        .AsAsyncEnumerable()
        .AsAsyncQueryable();

    public IntegrationReadRepository(ZeusDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Integration?> GetIntegrationByIdAsync(IntegrationId id)
    {
        return await Integrations.FirstOrDefaultAsync(integration => integration.Id == id);
    }

    public async Task<Page<Integration>> GetIntegrationsAsync(PageQuery query)
    {
        return await Integrations.PaginateAsync(query);
    }

    public Task<Page<Integration>> GetIntegrationsByOwnerIdAsync(UserId userId, PageQuery query)
    {
        var page = Integrations
            .Where(integration => integration.OwnerId == userId)
            .PaginateAsync(query);

        return page;
    }
}
