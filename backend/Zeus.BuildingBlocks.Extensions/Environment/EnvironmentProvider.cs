namespace Zeus.Common.Extensions.Environment;

internal class EnvironmentProvider: IEnvironmentProvider
{
    public EnvironmentProvider(string environmentName)
    {
        EnvironmentName = environmentName;
    }

    public string EnvironmentName { get; }
}
