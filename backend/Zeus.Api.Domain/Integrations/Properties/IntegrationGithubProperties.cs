namespace Zeus.Api.Domain.Integrations.Properties;

public record IntegrationGithubProperties(
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
    int Following) : IntegrationProperties;
