namespace Zeus.Api.Presentation.Web.Contracts.Common;

public record PageResponse<T>(
    int PageNumber,
    int PageSize,
    int TotalPages,
    int TotalRecords,
    T[] Data);
