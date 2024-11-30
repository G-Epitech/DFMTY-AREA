using Mapster;

using Zeus.Api.Application.Authentication.Common;
using Zeus.Api.Web.Contracts.Authentication;

namespace Zeus.Api.Web.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.AccessToken, src => src.AccessToken.Value)
            .Map(dest => dest.RefreshToken, src => src.RefreshToken.Value);
    }
}
