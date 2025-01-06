namespace Zeus.Common.Domain.Authentication.ValueObjects;

public sealed class AccessToken : Token
{
    public const string Type = "AccessToken";
    
    public AccessToken(string token) : base(token)
    {
    }
}
