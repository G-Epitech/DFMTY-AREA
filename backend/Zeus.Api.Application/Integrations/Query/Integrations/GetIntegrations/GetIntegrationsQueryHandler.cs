using ErrorOr;

using MediatR;

using Microsoft.Extensions.Logging;

using Zeus.Api.Application.Integrations.Query.Results;
using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Application.Interfaces.Services.Integrations;
using Zeus.Common.Domain.Integrations.IntegrationAggregate;
using Zeus.Common.Domain.UserAggregate.ValueObjects;
using Zeus.Common.Extensions.Queryable;

namespace Zeus.Api.Application.Integrations.Query.Integrations.GetIntegrations;

public class
    GetIntegrationsQueryHandler : IRequestHandler<GetIntegrationsQuery, ErrorOr<Page<GetIntegrationQueryResult>>>
{
    private readonly IIntegrationReadRepository _integrationReadRepository;
    private readonly IIntegrationService _integrationService;
    private readonly ILogger _logger;

    public GetIntegrationsQueryHandler(
        IIntegrationReadRepository integrationReadRepository,
        IIntegrationService integrationService,
        ILogger<GetIntegrationsQueryHandler> logger)
    {
        _integrationReadRepository = integrationReadRepository;
        _integrationService = integrationService;
        _logger = logger;
    }

    public async Task<ErrorOr<Page<GetIntegrationQueryResult>>> Handle(GetIntegrationsQuery query,
        CancellationToken cancellationToken)
    {
        var index = query.Index ?? 0;
        var limit = query.Limit ?? 10;

        var userId = new UserId(query.UserId);
        var pageQuery = new PageQuery { Index = index, Limit = limit };

        var integrations = await _integrationReadRepository.GetIntegrationsByOwnerIdAsync(userId, pageQuery);

        var integrationResultItems = new List<GetIntegrationQueryResult>();

        foreach (Integration integration in integrations.Items)
        {
            var propertiesResult = await _integrationService.GetProperties(integration);

            if (propertiesResult.IsError)
            {
                _logger.LogError("Unable to get integration properties: {IntegrationId}", integration.Id.Value);
                continue;
            }

            integrationResultItems.Add(new GetIntegrationQueryResult(
                integration.Id.Value,
                integration.OwnerId.Value,
                integration.Type.ToString(),
                integration.IsValid,
                propertiesResult.Value));
        }

        return new Page<GetIntegrationQueryResult>(
            integrations.Index, integrations.Size, integrations.TotalPages, integrations.TotalItems,
            integrationResultItems);
    }
}
