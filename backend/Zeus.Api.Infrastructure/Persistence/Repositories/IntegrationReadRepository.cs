using Microsoft.EntityFrameworkCore;

using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.AutomationAggregate.Enums;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
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

    private IQueryable<Automation> Automations => _dbContext.Automations.AsNoTracking();

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

    public async Task<Page<Integration>> GetIntegrationsByAutomationIdsAsync(
        IReadOnlyList<AutomationId> automationIds,
        AutomationIntegrationSource source = AutomationIntegrationSource.Any,
        CancellationToken cancellationToken = default)
    {
        var integrationIds = await (source switch
        {
            AutomationIntegrationSource.Trigger => Automations
                .Include(x => x.Trigger)
                .Where(a => automationIds.Contains(a.Id))
                .SelectMany(a => a.Trigger.Providers.Select(p => p)),
            AutomationIntegrationSource.Action => Automations
                .Where(a => automationIds.Contains(a.Id))
                .SelectMany(a => a.Actions.SelectMany(i => i.Providers)),
            _ => Automations
                .Where(a => automationIds.Contains(a.Id))
                .SelectMany(a => a.Trigger.Providers.Select(p => p).Union(a.Actions.SelectMany(ac => ac.Providers)))
        }).Distinct().ToListAsync(cancellationToken: cancellationToken);

        var integrations = await Integrations.Where(integration => integrationIds.Contains(integration.Id)).ToListAsync(cancellationToken);

        return new Page<Integration>(0, integrations.Count, 1, integrations.Count, integrations);
    }
}
