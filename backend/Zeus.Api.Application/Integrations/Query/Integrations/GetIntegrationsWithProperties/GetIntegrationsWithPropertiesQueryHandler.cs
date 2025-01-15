using ErrorOr;

using MediatR;

using Microsoft.Extensions.Logging;

using Zeus.Api.Application.Integrations.Query.Results;
using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Application.Interfaces.Services.Integrations;
using Zeus.Common.Domain.UserAggregate.ValueObjects;
using Zeus.Common.Extensions.Queryable;

namespace Zeus.Api.Application.Integrations.Query.Integrations.GetIntegrationsWithProperties;

public class
    GetIntegrationsWithPropertiesQueryHandler : IRequestHandler<GetIntegrationsWithPropertiesQuery,
    ErrorOr<Page<GetIntegrationWithPropertiesQueryResult>>>
{
    private readonly IIntegrationReadRepository _integrationReadRepository;
    private readonly IIntegrationService _integrationService;
    private readonly ILogger _logger;

    public GetIntegrationsWithPropertiesQueryHandler(
        IIntegrationReadRepository integrationReadRepository,
        IIntegrationService integrationService,
        ILogger<GetIntegrationsWithPropertiesQueryHandler> logger)
    {
        _integrationReadRepository = integrationReadRepository;
        _integrationService = integrationService;
        _logger = logger;
    }

    public async Task<ErrorOr<Page<GetIntegrationWithPropertiesQueryResult>>> Handle(
        GetIntegrationsWithPropertiesQuery withPropertiesQuery,
        CancellationToken cancellationToken)
    {
        var index = withPropertiesQuery.Index ?? 0;
        var limit = withPropertiesQuery.Limit ?? 10;

        var userId = new UserId(withPropertiesQuery.UserId);
        var pageQuery = new PageQuery { Index = index, Limit = limit };

        var integrations =
            await _integrationReadRepository.GetIntegrationsByOwnerIdAsync(userId, pageQuery, cancellationToken);

        var integrationResultItems = new List<GetIntegrationWithPropertiesQueryResult>();

        foreach (var integration in integrations.Items)
        {
            var propertiesResult = await _integrationService.GetProperties(integration);

            if (propertiesResult.IsError)
            {
                _logger.LogError("Unable to get integration properties: {IntegrationId}\n{Errors}",
                    integration.Id.Value, propertiesResult.Errors);
                continue;
            }

            integrationResultItems.Add(new GetIntegrationWithPropertiesQueryResult(
                integration.Id.Value,
                integration.OwnerId.Value,
                integration.Type.ToString(),
                integration.IsValid,
                propertiesResult.Value));
        }

        return new Page<GetIntegrationWithPropertiesQueryResult>(
            integrations.Index, integrations.Size, integrations.TotalPages, integrations.TotalItems,
            integrationResultItems);
    }
}
