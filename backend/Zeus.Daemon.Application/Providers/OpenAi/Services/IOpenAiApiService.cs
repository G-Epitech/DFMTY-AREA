using ErrorOr;

namespace Zeus.Daemon.Application.Providers.OpenAi.Services;

public interface IOpenAiApiService
{
    /// <summary>
    /// Generate a openAi completion
    /// </summary>
    /// <param name="context">The context of the completion (developer)</param>
    /// <param name="prompt">The prompt of the completion (user)</param>
    /// <param name="model">The id of the model to use</param>
    /// <param name="apiKey">The OpenAi user apikey</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The string completion</returns>
    public Task<ErrorOr<string>> GetCompletionAsync(string context, string prompt, string model, string apiKey,
        CancellationToken cancellationToken = default);
}
