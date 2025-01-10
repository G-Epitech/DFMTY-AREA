using ErrorOr;

using Zeus.Api.Domain.Integrations.Notion.ValueObjects;

namespace Zeus.Api.Application.Interfaces.Services.Integrations.Notion;

public interface INotionService
{
    /// <summary>
    /// Get notion tokens from oauth2 code.
    /// </summary>
    /// <param name="code">The code provided by the service</param>
    /// <returns>Notion workspace tokens</returns>
    public Task<ErrorOr<NotionWorkspaceTokens>> GetTokensFromOauth2Async(string code);
}
