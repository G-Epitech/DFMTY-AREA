using Grpc.Core;

using MapsterMapper;

using MediatR;

using Zeus.Api.Application.Automations.Query.GetAutomationById;
using Zeus.Api.Application.Integrations.Query.Integrations.GetIntegrationsByAutomationIds;
using Zeus.Api.Application.Synchronization.Queries.GetAutomationsUpdateAfter;
using Zeus.Api.Presentation.gRPC.Contracts;
using Zeus.Common.Domain.AutomationAggregate.Enums;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;

namespace Zeus.Api.Presentation.gRPC.Services;

public class AutomationsService : Contracts.AutomationsService.AutomationsServiceBase
{
    private readonly ILogger<AutomationsService> _logger;
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    public AutomationsService(ILogger<AutomationsService> logger, ISender sender, IMapper mapper)
    {
        _logger = logger;
        _sender = sender;
        _mapper = mapper;
    }

    public override async Task<Automation> GetAutomation(GetAutomationRequest request, ServerCallContext context)
    {
        var automationId = AutomationId.TryParse(request.Id);

        if (automationId is null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid automation id"));
        }

        var automation = await _sender.Send(new GetAutomationByIdQuery(automationId));
        if (automation.IsError)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Automation not found"));
        }
        return _mapper.Map<Automation>(automation.Value);
    }

    public override async Task GetAutomations(GetAutomationsRequest request, IServerStreamWriter<Automation> responseStream, ServerCallContext context)
    {
        var automations = await _sender.Send(new GetAutomationsUpdateAfterQuery(AutomationState.Any, DateTime.UnixEpoch));

        foreach (var automation in automations)
        {
            await responseStream.WriteAsync(_mapper.Map<Automation>(automation));
        }
    }

    public override async Task GetRegistrableAutomations(GetAutomationsRequest request,
        IServerStreamWriter<RegistrableAutomation> responseStream, ServerCallContext context)
    {
        var automations = await _sender.Send(new GetAutomationsUpdateAfterQuery(AutomationState.Any, DateTime.UnixEpoch));
        var triggers = await _sender.Send(new GetIntegrationsByAutomationIdsQuery(
            automations.Select(a => a.Id).ToList(),
            AutomationIntegrationSource.Trigger
        ));
        var triggersMap = triggers.Items.ToDictionary(t => t.Id, t => t);

        foreach (var automation in automations)
        {
            await responseStream.WriteAsync(new RegistrableAutomation
            {
                Automation = _mapper.Map<Automation>(automation),
                TriggerDependencies =
                {
                    triggersMap
                        .Where(p => automation.Trigger.Dependencies.Contains(p.Key))
                        .Select(p => _mapper.Map<Contracts.Integration>(p.Value))
                        .ToList()
                }
            });
        }
    }
}
