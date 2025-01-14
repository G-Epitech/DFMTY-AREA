using FluentValidation;

namespace Zeus.Api.Application.Integrations.Query.Integrations.GetIntegrationWithProperties;

public class GetIntegrationWithPropertiesQueryValidator : AbstractValidator<GetIntegrationWithPropertiesQuery>
{
    public GetIntegrationWithPropertiesQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
        RuleFor(x => x.IntegrationId)
            .NotEmpty();
    }
}
