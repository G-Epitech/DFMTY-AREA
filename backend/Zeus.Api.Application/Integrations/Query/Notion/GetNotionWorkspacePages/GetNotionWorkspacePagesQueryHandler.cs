using ErrorOr;

using MediatR;

using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Application.Interfaces.Services.Integrations;
using Zeus.Api.Domain.Errors.Integrations;
using Zeus.Common.Domain.Authentication.Common;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.Enums;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Application.Integrations.Query.Notion.GetNotionWorkspacePages;

public class GetNotionWorkspacePagesQueryHandler : IRequestHandler<GetNotionWorkspacePagesQuery,
    ErrorOr<List<GetNotionWorkspacePageQueryResult>>>
{
    private readonly INotionService _notionService;
    private readonly IIntegrationReadRepository _integrationReadRepository;

    public GetNotionWorkspacePagesQueryHandler(INotionService notionService,
        IIntegrationReadRepository integrationReadRepository)
    {
        _notionService = notionService;
        _integrationReadRepository = integrationReadRepository;
    }

    public async Task<ErrorOr<List<GetNotionWorkspacePageQueryResult>>> Handle(
        GetNotionWorkspacePagesQuery query,
        CancellationToken cancellationToken)
    {
        var integrationId = new IntegrationId(query.IntegrationId);
        var userId = new UserId(query.UserId);

        var integration = await _integrationReadRepository.GetIntegrationByIdAsync(integrationId, cancellationToken);

        if (integration is null || integration.OwnerId != userId)
        {
            return Errors.Integrations.NotFound;
        }

        var accessToken = integration.Tokens.First(x => x.Usage == IntegrationTokenUsage.Access);
        var pages = await _notionService.GetWorkspacePagesAsync(new AccessToken(accessToken.Value));

        if (pages.IsError)
        {
            return pages.Errors;
        }

        return pages.Value.Select(database => new GetNotionWorkspacePageQueryResult(
            database.Id.Value,
            database.Title,
            database.Icon,
            database.Uri)).ToList();
    }
}
