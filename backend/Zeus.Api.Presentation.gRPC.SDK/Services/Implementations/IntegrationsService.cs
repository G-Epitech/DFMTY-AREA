using Zeus.Api.Presentation.gRPC.Contracts;

namespace Zeus.Api.Presentation.gRPC.SDK.Services.Implementations;

using Client = Contracts.IntegrationsService.IntegrationsServiceClient;

internal class IntegrationsService : IIntegrationsService
{
    private readonly Client _client;

    public IntegrationsService(Client client)
    {
        _client = client;
    }

    public async Task<IList<Integration>> GetAutomationsIntegrationsAsync(IReadOnlyList<Guid> automationIds, IntegrationSource source = IntegrationSource.Any,
        CancellationToken cancellationToken = default)
    {
        if (automationIds.Count == 0)
        {
            return new List<Integration>();
        }

        var res = await _client.GetAutomationsIntegrationsAsync(
            new GetAutomationsIntegrationsRequest { AutomationIds = { automationIds.Select(id => id.ToString()).ToList() }, Source = source },
            cancellationToken: cancellationToken);

        return res.Integrations.ToList();
    }
}
