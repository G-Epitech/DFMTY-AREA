using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

using Json.Schema;

using Microsoft.Extensions.DependencyInjection;

namespace Zeus.Common.Domain.ProvidersSettings;

public static class DependencyInjection
{
    private static readonly Assembly Assembly = typeof(DependencyInjection).Assembly;
    private const string ProvidersSettingsSchemaResourceName = "Resources/providers-settings-schema.json";
    private const string ProvidersSettingsResourceName = "Resources/providers-settings.json";
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        Converters =
        {
            new JsonStringEnumConverter()
        }
    };

    private static async Task<JsonSchema> GetSchemaAsync()
    {
        var schemaStream = Assembly.GetManifestResourceStream(ProvidersSettingsSchemaResourceName);
        if (schemaStream == null)
            throw new InvalidOperationException($"Resource {ProvidersSettingsSchemaResourceName} not found.");

        return await JsonSchema.FromStream(schemaStream);
    }

    public static async Task<IServiceCollection> AddProvidersSettingsAsync(this IServiceCollection services)
    {
        try
        {
            var schema = await GetSchemaAsync();
            var settings = await GetSettingsAsync();

            var results = schema.Evaluate(settings);
            if (!results.IsValid)
            {
                LogErrorsDetails(results);
            }
            
            var deserializedSettings = settings.Deserialize<ProvidersSettings>(JsonSerializerOptions);

            if (deserializedSettings is null)
            {
                throw new InvalidOperationException("Providers schema deserialization failed");
            }

            services.AddSingleton(deserializedSettings);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return services;
    }

    private static void LogErrorsDetails(EvaluationResults results)
    {
        var currentColor = Console.ForegroundColor;

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Providers settings file is invalid");
        if (results.Errors != null)
        {
            Console.WriteLine("Details:");
            foreach (var error in results.Errors)
            {
                Console.WriteLine(error);
            }
        }
        Console.ForegroundColor = currentColor;
    }

    private static async Task<JsonNode> GetSettingsAsync()
    {
        await using var stream = Assembly.GetManifestResourceStream(ProvidersSettingsResourceName);
        if (stream == null)
        {
            throw new InvalidOperationException("Providers settings file not found");
        }

        var node = await JsonNode.ParseAsync(stream);
        if (node == null)
        {
            throw new InvalidOperationException("Providers settings file is empty");
        }
        return node;
    }
}
