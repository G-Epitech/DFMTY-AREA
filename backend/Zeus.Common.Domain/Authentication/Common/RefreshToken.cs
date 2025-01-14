namespace Zeus.Common.Domain.Authentication.Common;

public sealed class RefreshToken : Token
{
    public const string Type = "RefreshToken";

    public RefreshToken(string token) : base(token)
    {
    }
}
