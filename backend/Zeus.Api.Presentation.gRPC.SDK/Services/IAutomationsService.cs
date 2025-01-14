using Zeus.Api.Presentation.gRPC.Contracts;

namespace Zeus.Api.Presentation.gRPC.SDK.Services;

public interface IAutomationsService
{
    public IAsyncEnumerable<Automation> GetAutomationsAsync(AutomationEnabledState? state = null, Guid? ownerId = null, CancellationToken cancellationToken = default);

    public IAsyncEnumerable<RegistrableAutomation> GetRegistrableAutomationsAsync(AutomationEnabledState? state = null, Guid? ownerId = null,
        CancellationToken cancellationToken = default);
}
