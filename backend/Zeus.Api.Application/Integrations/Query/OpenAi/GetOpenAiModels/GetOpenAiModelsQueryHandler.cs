using ErrorOr;

using MediatR;

using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Application.Interfaces.Services.Integrations;
using Zeus.Api.Domain.Errors.Integrations;
using Zeus.Common.Domain.Authentication.Common;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.Enums;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Application.Integrations.Query.OpenAi.GetOpenAiModels;

public class
    GetOpenAiModelsQueryHandler : IRequestHandler<GetOpenAiModelsQuery, ErrorOr<List<GetOpenAiModelQueryResult>>>
{
    private readonly IOpenAiService _openAiService;
    private readonly IIntegrationReadRepository _integrationReadRepository;

    public GetOpenAiModelsQueryHandler(IOpenAiService openAiService,
        IIntegrationReadRepository integrationReadRepository)
    {
        _openAiService = openAiService;
        _integrationReadRepository = integrationReadRepository;
    }

    public async Task<ErrorOr<List<GetOpenAiModelQueryResult>>> Handle(GetOpenAiModelsQuery query,
        CancellationToken cancellationToken)
    {
        var integrationId = new IntegrationId(query.IntegrationId);
        var integration = await _integrationReadRepository.GetIntegrationByIdAsync(integrationId, cancellationToken);
        if (integration is null)
        {
            return Errors.Integrations.NotFound;
        }

        var userId = new UserId(query.UserId);
        if (integration.OwnerId != userId)
        {
            return Errors.Integrations.NotFound;
        }

        var accessToken =
            integration.Tokens.FirstOrDefault(x => x is { Usage: IntegrationTokenUsage.Access, Type: "Bearer" });
        if (accessToken is null)
        {
            return Errors.Integrations.OpenAi.ErrorDuringModelsRequest;
        }

        var models = await _openAiService.GetModelsAsync(new AccessToken(accessToken.Value));
        if (models.IsError)
        {
            return models.Errors;
        }

        return models.Value.Select(model => new GetOpenAiModelQueryResult(
            model.Id.Value)).ToList();
    }
}
