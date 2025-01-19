namespace Zeus.Api.Infrastructure.Services.Integrations.Github.Contracts;

public record GetGithubTokens(
    string AccessToken,
    string Scope,
    string TokenType);
