using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Infrastructure.Persistence.Interceptors;

public class AuditableEntitiesInterceptor : SaveChangesInterceptor
{
    private readonly ILogger _logger;

    public AuditableEntitiesInterceptor(ILogger<AuditableEntitiesInterceptor> logger)
    {
        _logger = logger;
    }

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

    private void UpdateAuditableEntities(DbContext dbContext)
    {
        var entries = dbContext.ChangeTracker
            .Entries<IAuditableEntity>()
            .Where(x => x.State is EntityState.Added or EntityState.Modified)
            .ToList();

        entries.ForEach(AuditEntity);
    }

    private void AuditEntity(EntityEntry<IAuditableEntity> entry)
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

    private void AuditUpdatedEntity(EntityEntry<IAuditableEntity> entry)
    {
        var entityType = entry.Entity.GetType();
        var updatedAtProperty = entityType.GetProperty(
            nameof(IAuditableEntity.UpdatedAt),
            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

        if (updatedAtProperty is { CanWrite: true })
        {
            updatedAtProperty.SetValue(entry.Entity, DateTime.UtcNow);
        }
        else
        {
            _logger.LogWarning(
                "Entity {Entity} was not audited because it is not writable.", entityType.Name);
        }
    }

    private void AuditCreatedEntity(EntityEntry<IAuditableEntity> entry)
    {
        var entityType = entry.Entity.GetType();
        var updatedAtProperty = entityType.GetProperty(
            nameof(IAuditableEntity.UpdatedAt),
            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        var createdAtProperty = entityType.GetProperty(
            nameof(IAuditableEntity.CreatedAt),
            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        var properties = new[]
        {
            createdAtProperty,
            updatedAtProperty
        };

        foreach (var property in properties)
        {
            if (property is { CanWrite: true })
            {
                property.SetValue(entry.Entity, DateTime.UtcNow);
            }
            else
            {
                _logger.LogWarning(
                    "Entity {Entity} was not audited because it is not writable.",
                    entityType.Name);
            }
        }
    }
}
