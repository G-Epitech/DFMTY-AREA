using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

using Zeus.Api.Infrastructure.Services;
using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Infrastructure.Persistence.Interceptors;

public class PublishDomainEventsInterceptor : SaveChangesInterceptor
{
    private readonly DomainEventDelayer _delayer;

    public PublishDomainEventsInterceptor(DomainEventDelayer delayer)
    {
        _delayer = delayer;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DelayDomainEvents(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
    {
        DelayDomainEvents(eventData.Context);

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void DelayDomainEvents(DbContext? dbContext)
    {
        if (dbContext is null)
        {
            return;
        }

        var entitiesWithDomainEvents = dbContext.ChangeTracker.Entries<IHasDomainEvents>()
            .Select(x => x.Entity)
            .Where(x => x.DomainEvents.Any())
            .ToList();

        var domainEvents = entitiesWithDomainEvents
            .SelectMany(x => x.DomainEvents)
            .ToList();

        entitiesWithDomainEvents.ForEach(x => x.ClearDomainEvents());

        _delayer.DelayEvents(domainEvents);
    }
}
