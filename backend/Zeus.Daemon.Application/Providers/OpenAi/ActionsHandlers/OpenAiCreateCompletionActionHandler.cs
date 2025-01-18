using Microsoft.Extensions.Logging;

using Zeus.Common.Domain.Integrations.IntegrationAggregate;
using Zeus.Daemon.Application.Attributes;
using Zeus.Daemon.Application.Execution;
using Zeus.Daemon.Application.Providers.OpenAi.Services;
using Zeus.Daemon.Domain.Automations;

namespace Zeus.Daemon.Application.Providers.OpenAi.ActionsHandlers;

public class OpenAiCreateCompletionActionHandler
{
    private readonly IOpenAiApiService _openAiApiService;

    public OpenAiCreateCompletionActionHandler(IOpenAiApiService openAiApiService,
        ILogger<OpenAiCreateCompletionActionHandler> logger)
    {
        _openAiApiService = openAiApiService;
    }

    [ActionHandler("OpenAi.CreateCompletion")]
    public async Task<ActionResult> RunAsync(
        [FromParameters] string model,
        [FromParameters] string context,
        [FromParameters] string prompt,
        [FromIntegrations] OpenAiIntegration openAiIntegration,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var apiKey = openAiIntegration.Tokens.FirstOrDefault(t => t.Type == "Bearer");
            if (apiKey is null)
            {
                return new ActionError { Message = $"Bearer token not found for OpenAI integration {openAiIntegration.Id.Value}" };
            }

            var completion =
                await _openAiApiService.GetCompletionAsync(context, prompt, model, apiKey.Value, cancellationToken);

            if (completion.IsError)
            {
                return new ActionError { Message = "An error occurred while generating the completion", Details = completion.FirstError.Description };
            }

            return new FactsDictionary { { "Completion", Fact.Create(completion.Value) } };
        }
        catch (Exception ex)
        {
            return new ActionError { Details = ex, InnerException = ex, Message = "An error occurred while generating the completion" };
        }
    }
}
