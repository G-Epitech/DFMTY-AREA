using ErrorOr;

using MediatR;

using Zeus.Api.Application.Integrations.Query.Results;

namespace Zeus.Api.Application.Integrations.Query.Integrations.GetIntegration;

public record GetIntegrationQuery(
    Guid UserId,
    Guid IntegrationId) : IRequest<ErrorOr<GetIntegrationQueryResult>>;
