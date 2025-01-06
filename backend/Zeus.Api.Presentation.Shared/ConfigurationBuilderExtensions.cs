using Microsoft.Extensions.Configuration;

namespace Zeus.Api.Presentation.Shared;

public static class ConfigurationBuilderExtensions
{
    private static readonly string BasePath = AppContext.BaseDirectory;

    public static IConfigurationBuilder AddSharedAppSettings(this IConfigurationBuilder builder)
    {
        return builder
            .AddJsonFile(
                Path.Combine(BasePath, "appsettings.shared.json"),
                optional: false,
                reloadOnChange: true
            ).AddJsonFile(
                Path.Combine(BasePath, $"appsettings.shared.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json"),
                optional: true,
                reloadOnChange: true
            );
    }
}
