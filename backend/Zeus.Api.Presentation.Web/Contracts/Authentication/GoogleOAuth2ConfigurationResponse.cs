namespace Zeus.Api.Presentation.Web.Contracts.Authentication;

public record GoogleOAuth2ConfigurationResponse(
    List<string> Scopes,
    string ClientId,
    Uri Endpoint);
