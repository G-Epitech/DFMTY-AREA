using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Domain.AutomationAggregate;
using Zeus.Api.Domain.AutomationAggregate.Enums;
using Zeus.Api.Domain.AutomationAggregate.ValueObjects;
using Zeus.Api.Domain.UserAggregate.ValueObjects;
using Zeus.Common.Extensions.Queryable;

namespace Zeus.Api.Infrastructure.Persistence.Repositories;

public sealed class AutomationReadRepository : IAutomationReadRepository
{
    private readonly ZeusDbContext _context;

    private IAsyncQueryable<Automation> Automations => _context.Automations.AsAsyncEnumerable().AsAsyncQueryable();

    public AutomationReadRepository(ZeusDbContext context)
    {
        _context = context;
    }

    public async Task<Automation?> GetByIdAsync(AutomationId id, CancellationToken cancellationToken = default)
    {
        return await Automations.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Page<Automation>> GetAutomationsByOwnerIdAsync(UserId ownerId, PageQuery query, CancellationToken cancellationToken = default)
    {
        return await Automations
            .Where(x => x.OwnerId == ownerId)
            .PaginateAsync(query, cancellationToken);
    }

    public async Task<Page<Automation>> GetAutomationsAsync(PageQuery query, CancellationToken cancellationToken = default)
    {
        return await Automations.PaginateAsync(query, cancellationToken);
    }

    public async Task<DateTime?> GetLastUpdateAsync(AutomationState state, UserId? ownerId, CancellationToken cancellationToken = default)
    {
        var expression = Automations;
        
        if (state != AutomationState.Any)
        {
            expression = expression.Where(x => x.Enabled == (state == AutomationState.Enabled));
        }
        if (ownerId is not null)
        {
            expression = expression.Where(x => x.OwnerId == ownerId);
        }
        
        return await expression.MaxAsync(x => x.UpdatedAt, cancellationToken);
    }

    public async Task<List<Automation>> GetAutomationsUpdatedAfterAsync(AutomationState state, DateTime lastUpdate, CancellationToken cancellationToken = default)
    {
        var expression = Automations
            .Where(x => x.UpdatedAt > lastUpdate);
        
        if (state != AutomationState.Any)
        {
            expression = expression.Where(x => x.Enabled == (state == AutomationState.Enabled));
        }
        
        return await expression.ToListAsync(cancellationToken);
    }
}
