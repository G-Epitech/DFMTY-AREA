using ErrorOr;

using MediatR;

using Zeus.Api.Application.Integrations.Query.GetIntegration.Results;

namespace Zeus.Api.Application.Integrations.Query.GetIntegration;

public record GetIntegrationQuery(
    Guid UserId,
    Guid IntegrationId) : IRequest<ErrorOr<GetIntegrationQueryResult>>;
