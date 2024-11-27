namespace Zeus.Api.Domain.Common.ValueObjects.Authentication;

public sealed class RefreshToken : Token
{
    public RefreshToken(string token) : base(token)
    {
    }
}
