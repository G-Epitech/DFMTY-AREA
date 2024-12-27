namespace Zeus.BuildingBlocks.Domain.Models;

public interface IAuditableEntity
{
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }
}
