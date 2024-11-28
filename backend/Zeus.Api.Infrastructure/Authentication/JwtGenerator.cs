using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Zeus.Api.Application.Common.Interfaces.Authentication;
using Zeus.Api.Application.Common.Interfaces.Services;
using Zeus.Api.Domain.Authentication.ValueObjects;
using Zeus.Api.Domain.UserAggregate;

namespace Zeus.Api.Infrastructure.Authentication;

public class JwtGenerator : IJwtGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JwtSettings _jwtSettings;

    public JwtGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtSettings)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtSettings = jwtSettings.Value;
    }

    private string GenerateToken(User user, string type, int expireMinutes)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.Value.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredCustomClaimNames.TokenType, type)
        };

        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: _dateTimeProvider.UtcNow.AddMinutes(expireMinutes),
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    public AccessToken GenerateAccessToken(User user)
    {
        return new AccessToken(GenerateToken(user, AccessToken.Type, _jwtSettings.AccessTokenExpiryMinutes));
    }

    public RefreshToken GenerateRefreshToken(User user)
    {
        return new RefreshToken(GenerateToken(user, RefreshToken.Type, _jwtSettings.RefreshTokenExpiryMinutes));
    }
}
