using System.Runtime.CompilerServices;

using Zeus.Api.Presentation.gRPC.Contracts;

namespace Zeus.Api.Presentation.gRPC.SDK.Services.Implementations;

using Client = Contracts.AutomationsService.AutomationsServiceClient;

internal class AutomationsService : IAutomationsService
{
    private readonly Client _client;

    public AutomationsService(Client client)
    {
        _client = client;
    }

    public async IAsyncEnumerable<Automation> GetAutomationsAsync(AutomationEnabledState? state = null, Guid? ownerId = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var request = new GetAutomationsRequest();

        if (state is not null)
        {
            request.State = state.Value;
        }

        if (ownerId is not null)
        {
            request.OwnerId = ownerId.Value.ToString();
        }

        var stream = _client.GetAutomations(request, cancellationToken: cancellationToken);

        if (stream is null)
        {
            yield break;
        }

        while (await stream.ResponseStream.MoveNext(cancellationToken) && !cancellationToken.IsCancellationRequested)
        {
            yield return stream.ResponseStream.Current;
        }
    }

    public async IAsyncEnumerable<RegistrableAutomation> GetRegistrableAutomationsAsync(AutomationEnabledState? state = null, Guid? ownerId = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var request = new GetAutomationsRequest();

        if (state is not null)
        {
            request.State = state.Value;
        }

        if (ownerId is not null)
        {
            request.OwnerId = ownerId.Value.ToString();
        }

        var stream = _client.GetRegistrableAutomations(request, cancellationToken: cancellationToken);

        if (stream is null)
        {
            yield break;
        }

        while (await stream.ResponseStream.MoveNext(cancellationToken) && !cancellationToken.IsCancellationRequested)
        {
            yield return stream.ResponseStream.Current;
        }
    }
}
