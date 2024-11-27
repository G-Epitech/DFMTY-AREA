namespace Zeus.Api.Domain.Common.ValueObjects.Authentication;

public sealed class AccessToken : Token
{
    public AccessToken(string token) : base(token)
    {
    }
}
