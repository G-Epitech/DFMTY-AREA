using FluentValidation;

namespace Zeus.Api.Application.Integrations.Query.GetIntegration;

public class GetIntegrationQueryValidator : AbstractValidator<GetIntegrationQuery>
{
    public GetIntegrationQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
        RuleFor(x => x.IntegrationId)
            .NotEmpty();
    }
}
