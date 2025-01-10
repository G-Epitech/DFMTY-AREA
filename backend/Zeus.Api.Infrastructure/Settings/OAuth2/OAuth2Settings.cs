namespace Zeus.Api.Infrastructure.Settings.OAuth2;

public class OAuth2Settings
{
    public const string SectionName = nameof(OAuth2Settings);
    
    public OAuth2GoogleSettings Google { get; init; } = null!;
}
