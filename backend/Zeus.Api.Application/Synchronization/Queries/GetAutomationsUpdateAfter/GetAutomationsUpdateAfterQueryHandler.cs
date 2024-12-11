using MediatR;

using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Domain.AutomationAggregate;

namespace Zeus.Api.Application.Synchronization.Queries.GetAutomationsUpdateAfter;

public class GetAutomationsUpdateAfterQueryHandler : IRequestHandler<GetAutomationsUpdateAfterQuery, List<Automation>>
{
    private readonly IAutomationReadRepository _automationReadRepository;

    public GetAutomationsUpdateAfterQueryHandler(IAutomationReadRepository automationReadRepository)
    {
        _automationReadRepository = automationReadRepository;
    }

    public async Task<List<Automation>> Handle(GetAutomationsUpdateAfterQuery request, CancellationToken cancellationToken)
    {
        return await _automationReadRepository.GetAutomationsUpdatedAfterAsync(
            request.State, request.LastUpdate, cancellationToken);
    }
}
