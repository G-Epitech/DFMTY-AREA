namespace Zeus.Api.Infrastructure.Authentication.Context;

public class AuthUserContext : IAuthUserContext
{
    public AuthUser? User { get; private set; }

    public void SetUser(AuthUser user)
    {
        if (User is not null)
        {
            return;
        }

        User = user;
    }
}
