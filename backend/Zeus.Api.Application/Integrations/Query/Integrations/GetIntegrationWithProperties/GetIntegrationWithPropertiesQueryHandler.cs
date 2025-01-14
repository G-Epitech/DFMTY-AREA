using ErrorOr;

using MapsterMapper;

using MediatR;

using Zeus.Api.Application.Integrations.Query.Results;
using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Application.Interfaces.Services.Integrations;
using Zeus.Api.Application.Interfaces.Services.Integrations.Discord;
using Zeus.Api.Domain.Errors.Integrations;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Application.Integrations.Query.Integrations.GetIntegrationWithProperties;

public class GetIntegrationWithPropertiesQueryHandler : IRequestHandler<GetIntegrationWithPropertiesQuery, ErrorOr<GetIntegrationWithPropertiesQueryResult>>
{
    private readonly IIntegrationReadRepository _integrationReadRepository;
    private readonly IIntegrationService _integrationService;

    public GetIntegrationWithPropertiesQueryHandler(IIntegrationReadRepository integrationReadRepository,
        IDiscordService discordService, IMapper mapper, IIntegrationService integrationService)
    {
        _integrationReadRepository = integrationReadRepository;
        _integrationService = integrationService;
    }

    public async Task<ErrorOr<GetIntegrationWithPropertiesQueryResult>> Handle(GetIntegrationWithPropertiesQuery withPropertiesQuery,
        CancellationToken cancellationToken)
    {
        var integrationId = new IntegrationId(withPropertiesQuery.IntegrationId);
        var userId = new UserId(withPropertiesQuery.UserId);

        var integration = await _integrationReadRepository.GetIntegrationByIdAsync(integrationId, cancellationToken);

        if (integration is null || integration.OwnerId != userId)
        {
            return Errors.Integrations.NotFound;
        }

        var propertiesResult = await _integrationService.GetProperties(integration);

        if (propertiesResult.IsError)
        {
            return propertiesResult.Errors;
        }

        return new GetIntegrationWithPropertiesQueryResult(
            integration.Id.Value,
            integration.OwnerId.Value,
            integration.Type.ToString(),
            integration.IsValid,
            propertiesResult.Value);
    }
}
