namespace Zeus.Common.Extensions.Queryable;

public static class QueryableExtensions
{
    private static void ValidatePageQuery(PageQuery query)
    {
        if (query.Index < 0)
            throw new ArgumentOutOfRangeException(nameof(query.Index), "Index must be greater than or equal to 0");
        if (query.Limit < 1)
            throw new ArgumentOutOfRangeException(nameof(query.Limit), "Limit must be greater than 0");
    }

    public static Page<T> Paginate<T>(this IQueryable<T> queryable, int index, int limit)
    {
        return queryable.Paginate(new PageQuery
        {
            Index = index,
            Limit = limit
        });
    }

    public static Page<T> Paginate<T>(this IQueryable<T> queryable, PageQuery query)
    {
        ValidatePageQuery(query);

        var items = queryable
            .Skip(query.Index * query.Limit)
            .Take(query.Limit);

        var totalItems = queryable.Count();
        var totalPages = (int) Math.Ceiling(totalItems / (double) query.Limit) + 1;
        
        return new Page<T>(query.Index, query.Limit, totalPages, totalItems, items.ToList());
    }

    public static async Task<Page<T>> PaginateAsync<T>(this IAsyncQueryable<T> queryable, int index, int size, CancellationToken cancellationToken = default)
    {
        return await queryable.PaginateAsync(new PageQuery
        {
            Index = index,
            Limit = size
        }, cancellationToken);
    }
    
    public static async Task<Page<T>> PaginateAsync<T>(this IAsyncQueryable<T> queryable, PageQuery query, CancellationToken cancellationToken = default)
    {
        ValidatePageQuery(query);

        var items = queryable
            .Skip(query.Index * query.Limit)
            .Take(query.Limit);

        var totalItems = await queryable.CountAsync(cancellationToken);
        var totalPages = (int) Math.Ceiling(totalItems / (double)query.Limit) + 1;
        var itemsList = await items.ToListAsync(cancellationToken);
        
        return new Page<T>(query.Index, query.Limit, totalPages, totalItems, itemsList);
    }
}
