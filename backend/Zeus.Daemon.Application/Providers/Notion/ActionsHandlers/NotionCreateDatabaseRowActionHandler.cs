using Zeus.Common.Domain.Authentication.Common;
using Zeus.Common.Domain.Integrations.IntegrationAggregate;
using Zeus.Daemon.Application.Attributes;
using Zeus.Daemon.Application.Execution;
using Zeus.Daemon.Application.Providers.Notion.Services;
using Zeus.Daemon.Domain.Automations;
using Zeus.Daemon.Domain.Providers.Notion.ValueObjects;

namespace Zeus.Daemon.Application.Providers.Notion.ActionsHandlers;

public class NotionCreateDatabaseRowActionHandler
{
    private readonly INotionApiService _notionApiService;

    public NotionCreateDatabaseRowActionHandler(INotionApiService notionApiService)
    {
        _notionApiService = notionApiService;
    }

    [ActionHandler("Notion.CreateDatabaseRow")]
    public async Task<ActionResult> RunAsync(
        [FromParameters] string databaseId,
        [FromParameters] string titleParamName,
        [FromParameters] string title,
        [FromParameters] string icon,
        [FromIntegrations] NotionIntegration notionIntegration,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var bearerToken = notionIntegration.Tokens.FirstOrDefault(t => t.Type == "Bearer");
            if (bearerToken is null)
            {
                return new ActionError
                {
                    Message = $"Bearer token not found for Notion integration {notionIntegration.Id.Value}"
                };
            }

            var accessToken = new AccessToken(bearerToken.Value);

            var page = await _notionApiService.CreatePageInDatabaseAsync(accessToken, new NotionDatabaseId(databaseId),
                titleParamName, title, icon, cancellationToken);

            if (page.IsError)
            {
                return new ActionError
                {
                    Message = "An error occurred while creating the database row",
                    Details = page.FirstError.Description
                };
            }

            return new FactsDictionary { { "Id", Fact.Create(page.Value.Id.Value) } };
        }
        catch (Exception ex)
        {
            return new ActionError
            {
                Details = ex, InnerException = ex, Message = "An error occurred while creating the database row"
            };
        }
    }
}
