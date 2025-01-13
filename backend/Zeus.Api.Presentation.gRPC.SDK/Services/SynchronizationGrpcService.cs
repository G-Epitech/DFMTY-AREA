using System.Runtime.CompilerServices;

using Grpc.Core;

using Zeus.Api.Presentation.gRPC.Contracts;

namespace Zeus.Api.Presentation.gRPC.SDK.Services;

public class AutomationService
{
    private readonly AutomationsService.AutomationsServiceClient _client;

    public AutomationService(AutomationsService.AutomationsServiceClient client)
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

        while (await stream.ResponseStream.MoveNext() && !cancellationToken.IsCancellationRequested)
        {
            yield return stream.ResponseStream.Current;
        }
    }
}
