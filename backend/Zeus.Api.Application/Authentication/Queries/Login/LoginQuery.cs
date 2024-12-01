using ErrorOr;

using MediatR;

namespace Zeus.Api.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<LoginQueryResult>>;
