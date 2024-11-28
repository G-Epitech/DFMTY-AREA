namespace Zeus.Api.Domain.Authentication.ValueObjects;

public sealed class RefreshToken : Token
{
    public const string Type = "RefreshToken";
    
    public RefreshToken(string token) : base(token)
    {
    }
}
