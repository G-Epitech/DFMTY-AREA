using System.Text.Json;

namespace Zeus.Api.Presentation.Web.Converters.Policies;

public class PascalCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        if (string.IsNullOrEmpty(name))
            return name;

        return char.ToUpper(name[0]) + name[1..];
    }
}
