using ErrorOr;

using Zeus.Api.Domain.Integrations.OpenAi;
using Zeus.Common.Domain.Authentication.Common;

namespace Zeus.Api.Application.Interfaces.Services.Integrations;

public interface IOpenAiService
{
    /// <summary>
    /// Get models available in OpenAI
    /// </summary>
    /// <param name="accessToken">The openAI Access Token</param>
    /// <returns>List of models</returns>
    public Task<ErrorOr<List<OpenAiModel>>> GetModelsAsync(AccessToken accessToken);

    /// <summary>
    /// Get users available in OpenAI
    /// </summary>
    /// <param name="accessToken">The openAI Access Token</param>
    /// <returns>List of users</returns>
    public Task<ErrorOr<List<OpenAiUser>>> GetUsersAsync(AccessToken accessToken);
}
