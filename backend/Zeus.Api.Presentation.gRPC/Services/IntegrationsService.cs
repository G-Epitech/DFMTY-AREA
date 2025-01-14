using Grpc.Core;

using MapsterMapper;

using MediatR;

using Zeus.Api.Application.Integrations.Query.Integrations.GetIntegrationsByAutomationIds;
using Zeus.Api.Presentation.gRPC.Contracts;
using Zeus.Common.Domain.AutomationAggregate.Enums;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;

namespace Zeus.Api.Presentation.gRPC.Services;

public class IntegrationsService : Contracts.IntegrationsService.IntegrationsServiceBase
{
    private readonly ILogger<IntegrationsService> _logger;
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    public IntegrationsService(ILogger<IntegrationsService> logger, ISender sender, IMapper mapper)
    {
        _logger = logger;
        _sender = sender;
        _mapper = mapper;
    }

    public override async Task<GetAutomationsIntegrationsResponse> GetAutomationsIntegrations(GetAutomationsIntegrationsRequest request, ServerCallContext context)
    {
        var automationIds = request.AutomationIds.Select(AutomationId.Parse).ToList();
        var result = await _sender.Send(new GetIntegrationsByAutomationIdsQuery(automationIds, request.Source switch
        {
            IntegrationSource.Action => AutomationIntegrationSource.Action,
            IntegrationSource.Trigger => AutomationIntegrationSource.Trigger,
            _ => AutomationIntegrationSource.Any
        }));

        return new GetAutomationsIntegrationsResponse { Integrations = { result.Items.Select(i => _mapper.Map<Contracts.Integration>(i)).ToList() } };
    }
}
