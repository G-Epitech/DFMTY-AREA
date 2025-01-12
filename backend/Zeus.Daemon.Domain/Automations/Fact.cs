using Zeus.Common.Domain.Common.Enums;

namespace Zeus.Daemon.Domain.Automations;

public abstract partial class Fact
{
    protected Fact(VariableType type)
    {
        Type = type;
    }

    public VariableType Type { get; init; }
}

public class Fact<T> : Fact
{
    internal Fact(T value, VariableType type): base(type)
    {
        Value = value;
    }

    public T Value { get; }
}

public abstract partial class Fact
{
    public static Fact<int> Create(int value) => new(value, VariableType.Integer);
    public static Fact<float> Create(float value) => new(value, VariableType.Float);
    public static Fact<string> Create(string value) => new(value, VariableType.String);
    public static Fact<bool> Create(bool value) => new(value, VariableType.Boolean);
    public static Fact<DateTime> Create(DateTime value) => new(value, VariableType.Datetime);
    public static Fact<object> Create(object value) => new(value, VariableType.Object);
}
