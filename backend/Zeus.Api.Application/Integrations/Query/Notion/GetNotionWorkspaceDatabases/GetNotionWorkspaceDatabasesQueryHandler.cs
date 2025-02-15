using ErrorOr;

using MediatR;

using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Application.Interfaces.Services.Integrations;
using Zeus.Api.Domain.Errors.Integrations;
using Zeus.Common.Domain.Authentication.Common;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.Enums;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Application.Integrations.Query.Notion.GetNotionWorkspaceDatabases;

public class GetNotionWorkspaceDatabasesQueryHandler : IRequestHandler<GetNotionWorkspaceDatabasesQuery,
    ErrorOr<List<GetNotionWorkspaceDatabaseQueryResult>>>
{
    private readonly IIntegrationReadRepository _integrationReadRepository;
    private readonly INotionService _notionService;

    public GetNotionWorkspaceDatabasesQueryHandler(INotionService notionService,
        IIntegrationReadRepository integrationReadRepository)
    {
        _notionService = notionService;
        _integrationReadRepository = integrationReadRepository;
    }

    public async Task<ErrorOr<List<GetNotionWorkspaceDatabaseQueryResult>>> Handle(
        GetNotionWorkspaceDatabasesQuery query,
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
        var databases = await _notionService.GetWorkspaceDatabasesAsync(new AccessToken(accessToken.Value));

        if (databases.IsError)
        {
            return databases.Errors;
        }

        return databases.Value.Select(database => new GetNotionWorkspaceDatabaseQueryResult(
            database.Id.Value,
            database.Title,
            database.Description,
            database.Icon,
            database.Uri)).ToList();
    }
}
