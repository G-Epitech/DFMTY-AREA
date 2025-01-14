using ErrorOr;

using MediatR;

using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Domain.Errors.Automations;
using Zeus.Common.Domain.AutomationAggregate;

namespace Zeus.Api.Application.Automations.Query.GetAutomationById;

public class GetAutomationByIdQueryHandler : IRequestHandler<GetAutomationByIdQuery, ErrorOr<Automation>>
{
    private readonly IAutomationReadRepository _automationReadRepository;

    public GetAutomationByIdQueryHandler(IAutomationReadRepository automationReadRepository)
    {
        _automationReadRepository = automationReadRepository;
    }

    public async Task<ErrorOr<Automation>> Handle(GetAutomationByIdQuery query, CancellationToken cancellationToken)
    {
        var automation = await _automationReadRepository.GetByIdAsync(query.AutomationId, cancellationToken);

        return automation is not null ? automation : Errors.Automations.NotFound;
    }
}
