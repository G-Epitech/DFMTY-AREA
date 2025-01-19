using FluentValidation;

namespace Zeus.Api.Application.Integrations.Query.OpenAi.GetOpenAiModels;

public class GetOpenAiModelsQueryValidator : AbstractValidator<GetOpenAiModelsQuery>
{
    public GetOpenAiModelsQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
        RuleFor(x => x.IntegrationId)
            .NotEmpty();
    }
}
