using ErrorOr;

using MediatR;

namespace Zeus.Api.Application.Integrations.Query.Notion.GetNotionWorkspaceDatabases;

public record GetNotionWorkspaceDatabasesQuery(
    Guid UserId,
    Guid IntegrationId) : IRequest<ErrorOr<List<GetNotionWorkspaceDatabaseQueryResult>>>;
