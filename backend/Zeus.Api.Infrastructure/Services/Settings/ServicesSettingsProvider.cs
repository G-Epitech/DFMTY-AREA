using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;

using Zeus.Api.Application.Interfaces.Services.Settings;
using Zeus.Api.Infrastructure.Settings;

namespace Zeus.Api.Infrastructure.Services.Settings;

public class ServicesSettingsProvider : IServicesSettingsProvider
{
    public Dictionary<string, IServicesSettingsProvider.IService> Services { get; }

    public ServicesSettingsProvider(IOptions<ServicesSettings> settings)
    {
        Services = settings.Value.Services.ToDictionary(
            service => service.Key,
            IServicesSettingsProvider.IService (service) => new Service(
                service.Value.Name,
                service.Value.IconUri,
                service.Value.Color,
                service.Value.Triggers,
                service.Value.Actions));
    }

    private class Service : IServicesSettingsProvider.IService
    {
        public string Name { get; }
        public string IconUri { get; }
        public string Color { get; }
        public Dictionary<string, IServicesSettingsProvider.ITrigger> Triggers { get; }
        public Dictionary<string, IServicesSettingsProvider.IAction> Actions { get; }

        public Service(string name, string iconUri, string color, Dictionary<string, ServicesSettings.Trigger> triggers,
            Dictionary<string, ServicesSettings.Action> actions)
        {
            Name = name;
            IconUri = iconUri;
            Color = color;
            Triggers = triggers.ToDictionary(
                trigger => trigger.Key,
                IServicesSettingsProvider.ITrigger (trigger) => new Trigger(
                    trigger.Value.Name,
                    trigger.Value.Description,
                    trigger.Value.Icon,
                    trigger.Value.Parameters,
                    trigger.Value.Facts));
            Actions = actions.ToDictionary(
                action => action.Key,
                IServicesSettingsProvider.IAction (action) => new Action(
                    action.Value.Name,
                    action.Value.Description,
                    action.Value.Icon,
                    action.Value.Parameters,
                    action.Value.Facts));
        }
    }

    private class Trigger : IServicesSettingsProvider.ITrigger
    {
        public string Name { get; }
        public string Description { get; }
        public string Icon { get; }
        public Dictionary<string, IServicesSettingsProvider.IParameter> Parameters { get; }
        public Dictionary<string, IServicesSettingsProvider.IFact> Facts { get; }

        public Trigger(string name, string description, string icon,
            Dictionary<string, ServicesSettings.Parameter> parameters,
            Dictionary<string, ServicesSettings.Fact> facts)
        {
            Name = name;
            Description = description;
            Icon = icon;
            Parameters = parameters.ToDictionary(
                parameter => parameter.Key,
                IServicesSettingsProvider.IParameter (parameter) => new Parameter(
                    parameter.Value.Name,
                    parameter.Value.Description,
                    parameter.Value.Type));
            Facts = facts.ToDictionary(
                fact => fact.Key,
                IServicesSettingsProvider.IFact (fact) => new Fact(
                    fact.Value.Name,
                    fact.Value.Description,
                    fact.Value.Type));
        }
    }

    private class Action : IServicesSettingsProvider.IAction
    {
        public string Name { get; }
        public string Description { get; }
        public string Icon { get; }
        public Dictionary<string, IServicesSettingsProvider.IParameter> Parameters { get; }
        public Dictionary<string, IServicesSettingsProvider.IFact> Facts { get; }

        public Action(string name, string description, string icon,
            Dictionary<string, ServicesSettings.Parameter> parameters,
            Dictionary<string, ServicesSettings.Fact> facts)
        {
            Name = name;
            Description = description;
            Icon = icon;
            Parameters = parameters.ToDictionary(
                parameter => parameter.Key,
                IServicesSettingsProvider.IParameter (parameter) => new Parameter(
                    parameter.Value.Name,
                    parameter.Value.Description,
                    parameter.Value.Type));
            Facts = facts.ToDictionary(
                fact => fact.Key,
                IServicesSettingsProvider.IFact (fact) => new Fact(
                    fact.Value.Name,
                    fact.Value.Description,
                    fact.Value.Type));
        }
    }

    private class Fact : IServicesSettingsProvider.IFact
    {
        public string Name { get; }
        public string Description { get; }
        public string Type { get; }

        public Fact(string name, string description, string type)
        {
            Name = name;
            Description = description;
            Type = type;
        }
    }

    private class Parameter : IServicesSettingsProvider.IParameter
    {
        public string Name { get; }
        public string Description { get; }
        public string Type { get; }

        public Parameter(string name, string description, string type)
        {
            Name = name;
            Description = description;
            Type = type;
        }
    }
}
