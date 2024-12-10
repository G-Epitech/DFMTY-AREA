using ErrorOr;

using MediatR;

using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Domain.AutomationAggregate;
using Zeus.Api.Domain.UserAggregate.ValueObjects;
using Zeus.Common.Extensions.Queryable;

namespace Zeus.Api.Application.Automations.Query.GetAutomations;

public class GetAutomationsQueryHandler : IRequestHandler<GetAutomationsQuery, ErrorOr<Page<Automation>>>
{
    private readonly IAutomationReadRepository _automationReadRepository;

    public GetAutomationsQueryHandler(IAutomationReadRepository automationReadRepository)
    {
        _automationReadRepository = automationReadRepository;
    }

    public async Task<ErrorOr<Page<Automation>>> Handle(GetAutomationsQuery query, CancellationToken cancellationToken)
    {
        var automations = await _automationReadRepository.GetAutomationsByOwnerIdAsync(
            new UserId(query.UserId),
            new PageQuery { Index = query.Index ?? 0, Limit = query.Limit ?? 10 },
            cancellationToken);

        return automations;
    }
}
