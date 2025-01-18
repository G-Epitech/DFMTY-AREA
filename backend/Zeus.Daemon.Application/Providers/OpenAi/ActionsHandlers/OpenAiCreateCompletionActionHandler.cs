using Microsoft.Extensions.Logging;

using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Domain.Integrations.IntegrationAggregate;
using Zeus.Daemon.Application.Attributes;
using Zeus.Daemon.Application.Providers.OpenAi.Services;
using Zeus.Daemon.Domain.Automations;

namespace Zeus.Daemon.Application.Providers.OpenAi.ActionsHandlers;

public class OpenAiCreateCompletionActionHandler
{
    private readonly IOpenAiApiService _openAiApiService;
    private readonly ILogger _logger;

    public OpenAiCreateCompletionActionHandler(IOpenAiApiService openAiApiService,
        ILogger<OpenAiCreateCompletionActionHandler> logger)
    {
        _openAiApiService = openAiApiService;
        _logger = logger;
    }

    [ActionHandler("OpenAi.CreateCompletion")]
    public async Task<FactsDictionary> RunAsync(
        AutomationId automationId,
        [FromParameters] string model,
        [FromParameters] string context,
        [FromParameters] string prompt,
        [FromIntegrations] OpenAiIntegration openAiIntegration,
        CancellationToken cancellationToken
    )
    {
        var apiKey = openAiIntegration.Tokens.FirstOrDefault(t => t.Type == "Bearer");
        if (apiKey is null)
        {
            _logger.LogError("Bearer token not found for OpenAI integration {IntegrationId}",
                openAiIntegration.Id.Value);
            return new FactsDictionary();
        }

        var completion =
            await _openAiApiService.GetCompletionAsync(context, prompt, model, apiKey.Value, cancellationToken);

        if (completion.IsError)
        {
            _logger.LogError("Error while generating OpenAI completion: {Error}", completion.Errors);
            return new FactsDictionary();
        }

        return new FactsDictionary { { "Completion", Fact.Create(completion.Value) } };
    }
}
