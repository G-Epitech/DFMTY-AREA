using Zeus.Common.Domain.Authentication.Common;
using Zeus.Common.Domain.UserAggregate;

namespace Zeus.Api.Application.Interfaces.Authentication;

public interface IJwtGenerator
{
    AccessToken GenerateAccessToken(User user);

    RefreshToken GenerateRefreshToken(User user);
}
