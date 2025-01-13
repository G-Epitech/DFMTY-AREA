using FluentValidation;

namespace Zeus.Api.Application.Integrations.Query.Notion.GetNotionWorkspacePages;

public class GetNotionWorkspacePagesQueryValidator : AbstractValidator<GetNotionWorkspacePagesQuery>
{
    public GetNotionWorkspacePagesQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
        RuleFor(x => x.IntegrationId)
            .NotEmpty();
    }
}
