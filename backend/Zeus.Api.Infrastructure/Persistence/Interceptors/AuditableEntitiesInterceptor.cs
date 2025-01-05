using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Infrastructure.Persistence.Interceptors;

public class AuditableEntitiesInterceptor : SaveChangesInterceptor
{

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is not null)
        {
            UpdateAuditableEntities(eventData.Context);
        }
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
    {
        if (eventData.Context is not null)
        {
            UpdateAuditableEntities(eventData.Context);
        }
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void UpdateAuditableEntities(DbContext dbContext)
    {
        var entries = dbContext.ChangeTracker
            .Entries<IAuditableEntity>()
            .Where(x => x.State is EntityState.Added or EntityState.Modified)
            .ToList();

        entries.ForEach(AuditEntity);
    }

    private static void AuditEntity(EntityEntry<IAuditableEntity> entry)
    {
        if (entry.State == EntityState.Added)
        {
            AuditCreatedEntity(entry);
        }
        if (entry.State is not (EntityState.Modified or EntityState.Added))
        {
            AuditUpdatedEntity(entry);
        }
    }

    private static void AuditUpdatedEntity(EntityEntry<IAuditableEntity> entry)
    {
        var updatedAtProperty = typeof(IAuditableEntity).GetProperty(
            nameof(IAuditableEntity.UpdatedAt),
            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

        updatedAtProperty?.SetValue(entry.Entity, DateTime.UtcNow);
    }

    private static void AuditCreatedEntity(EntityEntry<IAuditableEntity> entry)
    {
        var updatedAtProperty = typeof(IAuditableEntity).GetProperty(
            nameof(IAuditableEntity.UpdatedAt),
            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        var createdAtProperty = typeof(IAuditableEntity).GetProperty(
            nameof(IAuditableEntity.CreatedAt),
            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

        updatedAtProperty?.SetValue(entry.Entity, DateTime.UtcNow);
        createdAtProperty?.SetValue(entry.Entity, DateTime.UtcNow);
    }
}
