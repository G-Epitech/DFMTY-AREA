using FluentValidation;

using Zeus.Common.Domain.AutomationAggregate;

namespace Zeus.Api.Application.Automations.Commands.CreateAutomation;

public class CreateAutomationCommandValidator : AbstractValidator<CreateAutomationCommand>
{
    public CreateAutomationCommandValidator()
    {
        RuleFor(x => x.OwnerId.Value).NotEmpty();
        RuleFor(x => x.Label).NotEmpty().MaximumLength(Automation.LabelMaxLength);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(Automation.DescriptionMaxLength);
        RuleFor(x => x.Trigger).SetValidator(new TriggerValidator());
        RuleForEach(x => x.Actions).SetValidator(new ActionValidator());
    }

    private class TriggerValidator : AbstractValidator<CreateAutomationTriggerCommand>
    {
        public TriggerValidator()
        {
            RuleFor(x => x.Identifier).NotEmpty();
            RuleForEach(x => x.Parameters).ChildRules(p =>
            {
                p.RuleFor(x => x.Identifier).NotEmpty();
                p.RuleFor(x => x.Value).NotEmpty();
            });
            RuleForEach(x => x.Dependencies).ChildRules(g =>
            {
                g.RuleFor(x => x).NotEmpty();
            });
        }
    }

    private class ActionValidator : AbstractValidator<CreateAutomationActionCommand>
    {
        public ActionValidator()
        {
            RuleFor(x => x.Identifier).NotEmpty();
            RuleForEach(x => x.Parameters).ChildRules(p =>
            {
                p.RuleFor(x => x.Identifier).NotEmpty();
                p.RuleFor(x => x.Value).NotEmpty();
                p.RuleFor(x => x.Type).IsInEnum();
            });
            RuleForEach(x => x.Dependencies).ChildRules(g =>
            {
                g.RuleFor(x => x).NotEmpty();
            });
        }
    }
}
