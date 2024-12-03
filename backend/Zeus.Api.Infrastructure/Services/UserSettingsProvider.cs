using Microsoft.Extensions.Options;

using Zeus.Api.Application.Interfaces.Services;
using Zeus.Api.Infrastructure.Settings;

namespace Zeus.Api.Infrastructure.Services;

public class UserSettingsProvider : IUserSettingsProvider
{
    public UserSettingsProvider(IOptions<UserSettings> settings)
    {
        DefaultPicture = settings.Value.DefaultPicture;
    }
    
    public string DefaultPicture { get; }
}
