using Zeus.Api.Domain.Authentication.ValueObjects;
using Zeus.Api.Domain.UserAggregate;

namespace Zeus.Api.Application.Common.Interfaces.Authentication;

public interface IJwtGenerator
{
    AccessToken GenerateAccessToken(User user);
    
    RefreshToken GenerateRefreshToken(User user);
}