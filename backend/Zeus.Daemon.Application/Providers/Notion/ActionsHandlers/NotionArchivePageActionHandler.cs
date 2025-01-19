using Zeus.Common.Domain.Authentication.Common;
using Zeus.Common.Domain.Integrations.IntegrationAggregate;
using Zeus.Daemon.Application.Attributes;
using Zeus.Daemon.Application.Execution;
using Zeus.Daemon.Application.Providers.Notion.Services;
using Zeus.Daemon.Domain.Automations;
using Zeus.Daemon.Domain.Providers.Notion.ValueObjects;

namespace Zeus.Daemon.Application.Providers.Notion.ActionsHandlers;

public class NotionArchivePageActionHandler
{
    private readonly INotionApiService _notionApiService;

    public NotionArchivePageActionHandler(INotionApiService notionApiService)
    {
        _notionApiService = notionApiService;
    }

    [ActionHandler("Notion.ArchivePage")]
    public async Task<ActionResult> RunAsync(
        [FromParameters] string pageId,
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

            await _notionApiService.ArchivePageAsync(accessToken, new NotionPageId(pageId),
                cancellationToken);

            return new FactsDictionary();
        }
        catch (Exception ex)
        {
            return new ActionError
            {
                Details = ex,
                InnerException = ex,
                Message = "An error occurred while deleting the page"
            };
        }
    }
}
