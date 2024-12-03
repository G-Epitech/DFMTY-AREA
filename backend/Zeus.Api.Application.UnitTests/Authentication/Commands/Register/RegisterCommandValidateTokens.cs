using System.IdentityModel.Tokens.Jwt;

using FluentAssertions;

using Zeus.Api.Application.Authentication.Commands.Register;

namespace Zeus.Api.Application.UnitTests.Authentication.Commands.Register;

public static class RegisterCommandValidate
{
    public static void ValidateTokensFrom(this RegisterCommandResult commandResult, RegisterCommand command)
    {
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        commandResult.AccessToken.Should().NotBeNull();
        commandResult.RefreshToken.Should().NotBeNull();

        var accessToken = jwtSecurityTokenHandler.ReadToken(commandResult.AccessToken.Value) as JwtSecurityToken;
        accessToken.Should().NotBeNull();

        var claims = accessToken?.Claims.Select(c => (c.Type, c.Value)).ToList();
        claims?.Find(c => c.Type == JwtRegisteredClaimNames.GivenName).Value.Should().Be(command.FirstName);
        claims?.Find(c => c.Type == JwtRegisteredClaimNames.FamilyName).Value.Should().Be(command.LastName);
    }
}
