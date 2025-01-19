using FluentValidation;

namespace Zeus.Api.Application.Integrations.Commands.CreateGithubIntegration;

public class CreateGuthubIntegrationCommandValidator : AbstractValidator<CreateGithubIntegrationCommand>
{
    public CreateGuthubIntegrationCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty();
        RuleFor(x => x.State)
            .NotEmpty();
    }
}
