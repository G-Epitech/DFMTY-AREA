using ErrorOr;

using MediatR;

namespace Zeus.Api.Application.Integrations.Query.OpenAi.GetOpenAiModels;

public record GetOpenAiModelsQuery(
    Guid UserId,
    Guid IntegrationId) : IRequest<ErrorOr<List<GetOpenAiModelQueryResult>>>;
