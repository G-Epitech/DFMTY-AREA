using ErrorOr;

using MediatR;

using Zeus.Api.Application.Integrations.Query.Results;
using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Application.Interfaces.Services.Integrations;
using Zeus.Api.Domain.Integrations.IntegrationAggregate;
using Zeus.Api.Domain.UserAggregate.ValueObjects;
using Zeus.Common.Extensions.Queryable;

namespace Zeus.Api.Application.Integrations.Query.GetIntegrations;

public class
    GetIntegrationsQueryHandler : IRequestHandler<GetIntegrationsQuery, ErrorOr<Page<GetIntegrationQueryResult>>>
{
    private readonly IIntegrationReadRepository _integrationReadRepository;
    private readonly IIntegrationService _integrationService;

    public GetIntegrationsQueryHandler(IIntegrationReadRepository integrationReadRepository,
        IIntegrationService integrationService)
    {
        _integrationReadRepository = integrationReadRepository;
        _integrationService = integrationService;
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
                await Console.Error.WriteLineAsync($"Error with integration {integration.Id.Value}");
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
