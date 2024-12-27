namespace Zeus.BuildingBlocks.Domain.Models;

public abstract class AggregateRoot<TId> : AuditableEntity<TId>
    where TId : notnull
{
    protected AggregateRoot(TId id, DateTime updatedAt, DateTime createdAt)
        : base(id, updatedAt, createdAt)
    {
    }
#pragma warning disable CS8618
    protected AggregateRoot()
    {
    }
#pragma warning restore CS8618
}
