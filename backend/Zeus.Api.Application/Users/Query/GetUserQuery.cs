using ErrorOr;

using MediatR;

namespace Zeus.Api.Application.Users.Query;

public record GetUserQuery(
    Guid UserId) : IRequest<ErrorOr<GetUserQueryResult>>;
