using Mapster;

using Zeus.Api.Application.Authentication.Commands.Register;
using Zeus.Api.Application.Authentication.Queries.Login;
using Zeus.Api.Web.Contracts.Authentication;

namespace Zeus.Api.Web.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LoginQueryResult, AuthenticationResponse>()
            .Map(dest => dest.AccessToken, src => src.AccessToken.Value)
            .Map(dest => dest.RefreshToken, src => src.RefreshToken.Value);

        config.NewConfig<RegisterCommandResult, AuthenticationResponse>()
            .Map(dest => dest.AccessToken, src => src.AccessToken.Value)
            .Map(dest => dest.RefreshToken, src => src.RefreshToken.Value);
    }
}
