using Mapster;

using Zeus.Api.Application.Authentication.Commands.GoogleAuth;
using Zeus.Api.Application.Authentication.Commands.PasswordRegister;
using Zeus.Api.Application.Authentication.Queries.PasswordLogin;
using Zeus.Api.Presentation.Web.Contracts.Authentication;

namespace Zeus.Api.Presentation.Web.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<PasswordAuthLoginQueryResult, AuthenticationResponse>()
            .Map(dest => dest.AccessToken, src => src.AccessToken.Value)
            .Map(dest => dest.RefreshToken, src => src.RefreshToken.Value);

        config.NewConfig<PasswordAuthRegisterCommandResult, AuthenticationResponse>()
            .Map(dest => dest.AccessToken, src => src.AccessToken.Value)
            .Map(dest => dest.RefreshToken, src => src.RefreshToken.Value);
        config.NewConfig<GoogleAuthCommandResult, AuthenticationResponse>()
            .Map(dest => dest.AccessToken, src => src.AccessToken.Value)
            .Map(dest => dest.RefreshToken, src => src.RefreshToken.Value);
    }
}
