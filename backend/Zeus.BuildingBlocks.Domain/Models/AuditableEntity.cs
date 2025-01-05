namespace Zeus.BuildingBlocks.Domain.Models;

public abstract class AuditableEntity<TId> : Entity<TId>, IAuditableEntity
    where TId : notnull
{
    public DateTime CreatedAt { get; protected set; }
    public DateTime UpdatedAt { get; protected set; }

    protected AuditableEntity(TId id, DateTime updatedAt, DateTime createdAt)
        : base(id)
    {
        UpdatedAt = updatedAt;
        CreatedAt = createdAt;
    }

#pragma warning disable CS8618
    protected AuditableEntity()
    {
    }
#pragma warning restore CS8618
}
