using Zeus.Common.Domain.Authentication.ValueObjects;
using Zeus.Common.Domain.UserAggregate;

namespace Zeus.Api.Application.Interfaces.Authentication;

public interface IJwtGenerator
{
    AccessToken GenerateAccessToken(User user);
    
    RefreshToken GenerateRefreshToken(User user);
}
