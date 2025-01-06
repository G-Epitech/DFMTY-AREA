namespace Zeus.Daemon.Infrastructure.Services.Settings;

public class GRpcApiSettings
{
    public const string SectionName = "GRpcApiSettings";

    public string? Method { get; set; }
    public string? Host { get; set; }
    public ushort Port { get; set; }

    public string Address
    {
        get
        {
            if (string.IsNullOrWhiteSpace(Method))
            {
                throw new InvalidOperationException("Method is not set");
            }
            if (string.IsNullOrWhiteSpace(Host))
            {
                throw new InvalidOperationException("Host is not set");
            }
            return $"{Method}://{Host}:{Port}";
        }
    }
}
