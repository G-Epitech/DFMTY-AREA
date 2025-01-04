namespace Zeus.Common.Extensions.Queryable;

public record Page<T>(
    int Index,
    int Size,
    int TotalPages,
    int TotalItems,
    IReadOnlyCollection<T> Items);
