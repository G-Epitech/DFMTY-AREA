using ErrorOr;

using MediatR;

using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Domain.Errors.Automations;
using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Application.Automations.Query.GetAutomation;

public class GetAutomationQueryHandler : IRequestHandler<GetAutomationQuery, ErrorOr<Automation>>
{
    private readonly IAutomationReadRepository _automationReadRepository;

    public GetAutomationQueryHandler(IAutomationReadRepository automationReadRepository)
    {
        _automationReadRepository = automationReadRepository;
    }

    public async Task<ErrorOr<Automation>> Handle(GetAutomationQuery query, CancellationToken cancellationToken)
    {
        var automation = await _automationReadRepository.GetByIdAsync(
            new AutomationId(query.AutomationId),
            cancellationToken);

        if (automation is null || automation.OwnerId != new UserId(query.UserId))
        {
            return Errors.Automations.NotFound;
        }

        return automation;
    }
}
