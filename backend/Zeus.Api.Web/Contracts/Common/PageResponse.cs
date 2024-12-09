namespace Zeus.Api.Web.Contracts.Common;

public record PageResponse<T>(
    int PageNumber,
    int PageSize,
    int TotalPages,
    int TotalRecords,
    T[] Data);
