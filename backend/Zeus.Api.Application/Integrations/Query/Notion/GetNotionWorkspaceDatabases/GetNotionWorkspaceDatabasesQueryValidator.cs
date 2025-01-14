using FluentValidation;

namespace Zeus.Api.Application.Integrations.Query.Notion.GetNotionWorkspaceDatabases;

public class GetNotionWorkspaceDatabasesQueryValidator : AbstractValidator<GetNotionWorkspaceDatabasesQuery>
{
    public GetNotionWorkspaceDatabasesQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
        RuleFor(x => x.IntegrationId)
            .NotEmpty();
    }
}
