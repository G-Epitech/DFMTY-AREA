using Zeus.Api.Domain.Authentication.ValueObjects;

namespace Zeus.Api.Domain.Common.ValueObjects.Authentication;

public sealed class RefreshToken : Token
{
    public const string Type = "RefreshToken";
    
    public RefreshToken(string token) : base(token)
    {
    }
}
