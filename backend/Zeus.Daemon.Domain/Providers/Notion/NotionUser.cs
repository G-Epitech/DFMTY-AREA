using Zeus.BuildingBlocks.Domain.Models;
using Zeus.Daemon.Domain.Providers.Notion.ValueObjects;

namespace Zeus.Daemon.Domain.Providers.Notion;

public class NotionUser : Entity<NotionUserId>
{
    private NotionUser(NotionUserId id, string name, Uri avatarUri, string email)
        : base(id)
    {
        Name = name;
        AvatarUri = avatarUri;
        Email = email;
    }

    public string Name { get; private set; }
    public Uri AvatarUri { get; private set; }
    public string Email { get; private set; }

    public static NotionUser Create(NotionUserId id, string name, Uri? avatarUri, string email)
    {
        avatarUri ??= new Uri($"https://ui-avatars.com/api/?name={Uri.EscapeDataString(name)}&size=128");

        return new NotionUser(id, name, avatarUri, email);
    }
}
