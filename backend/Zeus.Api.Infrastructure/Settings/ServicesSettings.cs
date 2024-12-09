namespace Zeus.Api.Infrastructure.Settings;

public class ServicesSettings
{
    public const string SectionName = nameof(ServicesSettings);

    public Dictionary<string, Service> Services { get; init; } = null!;

    public class Service
    {
        public string Name { get; init; } = null!;
        public string IconUri { get; init; } = null!;
        public string Color { get; init; } = null!;
        public Dictionary<string, Event> Events { get; init; } = null!;
        public Dictionary<string, Action> Actions { get; init; } = null!;
    }

    public class Event
    {
        public string Name { get; init; } = null!;
        public string Description { get; init; } = null!;
        public string Icon { get; init; } = null!;
        public Dictionary<string, Parameter> Parameters { get; init; } = null!;
        public Dictionary<string, Fact> Facts { get; init; } = null!;
    }

    public class Action
    {
        public string Name { get; init; } = null!;
        public string Description { get; init; } = null!;
        public string Icon { get; init; } = null!;
        public Dictionary<string, Parameter> Parameters { get; init; } = null!;
        public Dictionary<string, Fact> Facts { get; init; } = null!;
    }

    public class Parameter
    {
        public string Name { get; init; } = null!;
        public string Description { get; init; } = null!;
        public string Type { get; init; } = null!;
    }

    public class Fact
    {
        public string Name { get; init; } = null!;
        public string Description { get; init; } = null!;
        public string Type { get; init; } = null!;
    }
}
