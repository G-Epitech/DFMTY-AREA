using ErrorOr;

using MediatR;

using Zeus.Api.Application.Authentication.Common;

namespace Zeus.Api.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;
