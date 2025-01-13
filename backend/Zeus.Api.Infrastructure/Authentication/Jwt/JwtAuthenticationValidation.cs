using System.Security.Claims;
using System.Text;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using Zeus.Api.Application.Users.Query;
using Zeus.Api.Infrastructure.Authentication.Context;
using Zeus.Api.Infrastructure.Settings;

namespace Zeus.Api.Infrastructure.Authentication.Jwt;

public class JwtAuthenticationValidation
{
    public static TokenValidationParameters GetTokenValidationParameters(JwtSettings settings)
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = settings.Issuer,
            ValidAudience = settings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret))
        };
    }

    public static JwtBearerEvents GetJwtBearerEvents()
    {
        return new JwtBearerEvents()
        {
            OnTokenValidated = async context =>
            {
                var sender = context.HttpContext.RequestServices.GetRequiredService<ISender>();
                var userId = context.Principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var parsedUserId))
                {
                    context.Fail("Invalid user id");
                    return;
                }

                var queryResponse = await sender.Send(new GetUserQuery(parsedUserId));

                if (queryResponse.IsError)
                {
                    context.Fail("Invalid user id");
                    return;
                }

                var authUserContext = context.HttpContext.RequestServices.GetRequiredService<IAuthUserContext>();
                var mapper = context.HttpContext.RequestServices.GetRequiredService<IMapper>();

                authUserContext.SetUser(mapper.Map<AuthUser>(queryResponse.Value));
            }
        };
    }
}
