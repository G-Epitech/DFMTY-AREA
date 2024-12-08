using Microsoft.Extensions.Options;

using Zeus.Api.Application.Interfaces.Services.Settings;
using Zeus.Api.Infrastructure.Settings;

namespace Zeus.Api.Infrastructure.Services.Settings;

public class UserSettingsProvider : IUserSettingsProvider
{
    public UserSettingsProvider(IOptions<UserSettings> settings)
    {
        DefaultPicture = settings.Value.DefaultPicture;
    }
    
    public string DefaultPicture { get; }
}
