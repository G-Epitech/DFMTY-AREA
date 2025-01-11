namespace Zeus.Api.Presentation.Web.Contracts.Authentication;

public record GoogleOAuth2ConfigurationResponse(
    List<string> Scopes,
    List<GoogleOAuth2ClientIdConfigurationResponse> ClientIds,
    Uri Endpoint);

public record GoogleOAuth2ClientIdConfigurationResponse(
    GoogleOAuth2ClientIdProvider Provider,
    string ClientId);

public enum GoogleOAuth2ClientIdProvider
{
    Web,
    Android,
    Ios
}
