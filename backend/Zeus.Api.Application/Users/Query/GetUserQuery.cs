using ErrorOr;

using MediatR;

using Zeus.Api.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Application.Users.Query;

public record GetUserQuery(
    Guid UserId) : IRequest<ErrorOr<GetUserQueryResult>>;
