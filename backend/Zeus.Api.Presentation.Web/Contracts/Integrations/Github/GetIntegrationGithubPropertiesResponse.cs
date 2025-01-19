namespace Zeus.Api.Presentation.Web.Contracts.Integrations.Github;

public record GetIntegrationGithubPropertiesResponse(
    Int64 Id,
    string Name,
    string? Email,
    string? Bio,
    Uri AvatarUri,
    Uri ProfileUri,
    string? Company,
    string? Blog,
    string? Location,
    int Followers,
    int Following) : GetIntegrationPropertiesResponse;
