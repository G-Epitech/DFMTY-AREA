using System.Text.Json;
using System.Text.Json.Serialization;

using Zeus.Api.Presentation.Web.Converters.Policies;
using Zeus.Common.Domain.ProvidersSettings;

namespace Zeus.Api.Presentation.Web.Converters;

public class JsonProvidersSettingsConverter : JsonConverter<ProvidersSettings>
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = new PascalCaseNamingPolicy(),
        DictionaryKeyPolicy = new PascalCaseNamingPolicy(),
        Converters = { new JsonStringEnumConverter() }
    };

    public override ProvidersSettings? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<ProvidersSettings>(ref reader, options);
    }

    public override void Write(Utf8JsonWriter writer, ProvidersSettings value, JsonSerializerOptions currentOptions)
    {
        JsonSerializer.Serialize(writer, value, Options);
    }
}
