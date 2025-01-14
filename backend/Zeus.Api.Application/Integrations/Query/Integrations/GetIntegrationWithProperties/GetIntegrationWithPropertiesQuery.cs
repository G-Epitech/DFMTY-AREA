using ErrorOr;

using MediatR;

using Zeus.Api.Application.Integrations.Query.Results;

namespace Zeus.Api.Application.Integrations.Query.Integrations.GetIntegrationWithProperties;

public record GetIntegrationWithPropertiesQuery(
    Guid UserId,
    Guid IntegrationId) : IRequest<ErrorOr<GetIntegrationWithPropertiesQueryResult>>;
