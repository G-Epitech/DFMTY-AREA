using ErrorOr;

using MediatR;

using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Application.Authentication.Queries.RefreshTokens;

public record RefreshTokensQuery(UserId UserId): IRequest<ErrorOr<RefreshTokensQueryResult>>;
