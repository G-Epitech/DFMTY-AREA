using MediatR;

using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Common.Extensions.Queryable;

namespace Zeus.Api.Application.Integrations.Query.Integrations.GetIntegrationsByAutomationIds;

using Integration = Common.Domain.Integrations.IntegrationAggregate.Integration;

public class
    GetIntegrationsByAutomationIdsQueryHandler : IRequestHandler<GetIntegrationsByAutomationIdsQuery, Page<Integration>>
{
    private readonly IIntegrationReadRepository _integrationReadRepository;

    public GetIntegrationsByAutomationIdsQueryHandler(
        IIntegrationReadRepository integrationReadRepository)
    {
        _integrationReadRepository = integrationReadRepository;
    }

    public async Task<Page<Integration>> Handle(GetIntegrationsByAutomationIdsQuery query,
        CancellationToken cancellationToken)
    {
        if (query.AutomationIds.Count == 0)
        {
            return Page<Integration>.Empty;
        }

        return await _integrationReadRepository.GetIntegrationsByAutomationIdsAsync(query.AutomationIds, query.Source, cancellationToken);
    }
}
