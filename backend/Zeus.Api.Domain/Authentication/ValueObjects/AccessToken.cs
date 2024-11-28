using Zeus.Api.Domain.Authentication.ValueObjects;

namespace Zeus.Api.Domain.Common.ValueObjects.Authentication;

public sealed class AccessToken : Token
{
    public const string Type = "AccessToken";
    
    public AccessToken(string token) : base(token)
    {
    }
}
