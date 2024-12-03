namespace Zeus.Api.Infrastructure.Settings;

public class UserSettings
{
    public const string SectionName = nameof(UserSettings);
    
    public string DefaultPicture { get; init; } = null!;
}
