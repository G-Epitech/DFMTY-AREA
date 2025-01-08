using ErrorOr;

using MediatR;

namespace Zeus.Api.Application.Authentication.Queries.Login;

public record PasswordAuthLoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<PasswordAuthLoginQueryResult>>;
