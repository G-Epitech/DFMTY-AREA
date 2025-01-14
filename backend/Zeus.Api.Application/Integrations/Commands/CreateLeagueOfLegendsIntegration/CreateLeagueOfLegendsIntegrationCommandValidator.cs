using FluentValidation;

namespace Zeus.Api.Application.Integrations.Commands.CreateLeagueOfLegendsIntegration;

public class
    CreateLeagueOfLegendsIntegrationCommandValidator : AbstractValidator<CreateLeagueOfLegendsIntegrationCommand>
{
    public CreateLeagueOfLegendsIntegrationCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.GameName).NotEmpty();
        RuleFor(x => x.TagLine).NotEmpty();
    }
}
