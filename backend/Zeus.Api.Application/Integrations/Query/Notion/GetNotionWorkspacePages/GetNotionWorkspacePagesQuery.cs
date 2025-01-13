using ErrorOr;

using MediatR;

namespace Zeus.Api.Application.Integrations.Query.Notion.GetNotionWorkspacePages;

public record GetNotionWorkspacePagesQuery(
    Guid UserId,
    Guid IntegrationId) : IRequest<ErrorOr<List<GetNotionWorkspacePageQueryResult>>>;
