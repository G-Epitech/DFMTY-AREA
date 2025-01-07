using ErrorOr;

using MediatR;

using Zeus.Api.Application.Integrations.Query.Results;
using Zeus.Common.Extensions.Queryable;

namespace Zeus.Api.Application.Integrations.Query.Integrations.GetIntegrations;

public record GetIntegrationsQuery(
    Guid UserId,
    int? Index,
    int? Limit) : IRequest<ErrorOr<Page<GetIntegrationQueryResult>>>;
