using ErrorOr;

using MediatR;

using Zeus.Api.Application.Integrations.Query.Results;
using Zeus.Common.Extensions.Queryable;

namespace Zeus.Api.Application.Integrations.Query.Integrations.GetIntegrationsWithProperties;

public record GetIntegrationsWithPropertiesQuery(
    Guid UserId,
    int? Index,
    int? Limit) : IRequest<ErrorOr<Page<GetIntegrationWithPropertiesQueryResult>>>;
