namespace Zeus.Api.Application.Interfaces.Services.Settings;

public interface IServicesSettingsProvider
{
    public Dictionary<string, IService> Services { get; }

    public interface IService
    {
        public string Name { get; }
        public string IconUri { get; }
        public string Color { get; }
        public Dictionary<string, ITrigger> Triggers { get; }
        public Dictionary<string, IAction> Actions { get; }
    }

    public interface ITrigger
    {
        public string Name { get; }
        public string Description { get; }
        public string Icon { get; }
        public Dictionary<string, IParameter> Parameters { get; }
        public Dictionary<string, IFact> Facts { get; }
    }

    public interface IAction
    {
        public string Name { get; }
        public string Description { get; }
        public string Icon { get; }
        public Dictionary<string, IParameter> Parameters { get; }
        public Dictionary<string, IFact> Facts { get; }
    }

    public interface IParameter
    {
        public string Name { get; }
        public string Description { get; }
        public string Type { get; }
    }

    public interface IFact
    {
        public string Name { get; }
        public string Description { get; }
        public string Type { get; }
    }
}
