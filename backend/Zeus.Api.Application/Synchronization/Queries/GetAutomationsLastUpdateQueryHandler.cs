using MediatR;

using Zeus.Api.Application.Interfaces.Repositories;

namespace Zeus.Api.Application.Synchronization.Queries;

public class GetAutomationsLastUpdateQueryHandler : IRequestHandler<GetAutomationsLastUpdateQuery, DateTime?>
{
    private readonly IAutomationReadRepository _automationReadRepository;

    public GetAutomationsLastUpdateQueryHandler(IAutomationReadRepository automationReadRepository)
    {
        _automationReadRepository = automationReadRepository;
    }

    public async Task<DateTime?> Handle(GetAutomationsLastUpdateQuery request, CancellationToken cancellationToken)
    {
        var lastUpdate = await _automationReadRepository.GetLastUpdateAsync(
            request.State, request.OwnerId, cancellationToken);

        return lastUpdate;
    }
}
