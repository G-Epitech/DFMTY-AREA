namespace Zeus.Api.Application.Interfaces.Services.Settings.OAuth2;

public interface IOAuth2SettingsProvider
{
    public IOAuth2GoogleSettingsProvider Google { get; }
}
