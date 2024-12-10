using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Domain.AutomationAggregate;

namespace Zeus.Api.Infrastructure.Persistence.Repositories;

public sealed class AutomationWriteRepository : IAutomationWriteRepository
{
    private readonly ZeusDbContext _dbContext;

    public AutomationWriteRepository(ZeusDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAutomationAsync(Automation automation, CancellationToken cancellationToken = default)
    {
        _dbContext.Automations.Add(automation);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAutomationAsync(Automation automation, CancellationToken cancellationToken = default)
    {
        _dbContext.Automations.Update(automation);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAutomationAsync(Automation automation, CancellationToken cancellationToken = default)
    {
        _dbContext.Automations.Remove(automation);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
