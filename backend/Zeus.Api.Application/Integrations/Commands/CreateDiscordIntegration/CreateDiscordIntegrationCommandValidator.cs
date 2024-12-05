using FluentValidation;

namespace Zeus.Api.Application.Integrations.Commands.CreateDiscordIntegration;

public class CreateDiscordIntegrationCommandValidator : AbstractValidator<CreateDiscordIntegrationCommand>
{
    public CreateDiscordIntegrationCommandValidator()
    {
        RuleFor(x => x.Code).NotEmpty();
        RuleFor(x => x.State).NotEmpty();
    }
}
