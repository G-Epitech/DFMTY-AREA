namespace Zeus.Api.Infrastructure.Services.Integrations.Github.Contracts;

public record GetGithubTokensResult(
    string AccessToken,
    string Scope,
    string TokenType);
