namespace Zeus.Common.Domain.Authentication.Common;

public sealed class AccessToken : Token
{
    public const string Type = "AccessToken";
    
    public AccessToken(string token) : base(token)
    {
    }
}
