namespace Zeus.Api.Infrastructure.Authentication.Context;

public interface IAuthUserContext
{
    AuthUser? User { get; }
    
    public void SetUser(AuthUser user);
}
