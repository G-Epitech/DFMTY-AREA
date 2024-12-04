using ErrorOr;

using MediatR;

using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Domain.Integrations.IntegrationLinkRequestAggregate;
using Zeus.Api.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Application.Integrations.Commands.CreateIntegrationLinkRequest;

public class CreateIntegrationLinkRequestCommandHandler : IRequestHandler<CreateIntegrationLinkRequestCommand,
    ErrorOr<CreateIntegrationLinkRequestCommandResult>>
{
    private readonly IIntegrationWriteRepository _integrationWriteRepository;

    public CreateIntegrationLinkRequestCommandHandler(IIntegrationWriteRepository integrationWriteRepository)
    {
        _integrationWriteRepository = integrationWriteRepository;
    }

    public async Task<ErrorOr<CreateIntegrationLinkRequestCommandResult>> Handle(
        CreateIntegrationLinkRequestCommand command,
        CancellationToken cancellationToken)
    {
        var integrationLinkRequest =
            IntegrationLinkRequest.Create(new UserId(command.UserId), command.Type.ToIntegrationType());

        await _integrationWriteRepository.AddIntegrationLinkRequestAsync(integrationLinkRequest);

        return new CreateIntegrationLinkRequestCommandResult(integrationLinkRequest.Id.Value);
    }
}
