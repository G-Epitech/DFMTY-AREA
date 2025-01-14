using Zeus.Api.Presentation.gRPC.Contracts;

namespace Zeus.Api.Presentation.gRPC.SDK.Services;

public interface IIntegrationsService
{
    public Task<IList<Integration>> GetAutomationsIntegrationsAsync(IReadOnlyList<Guid> automationIds, IntegrationSource source = IntegrationSource.Any,
        CancellationToken cancellationToken = default);
}
