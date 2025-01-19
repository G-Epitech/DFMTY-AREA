using Zeus.Api.Domain.Integrations.Gmail.ValueObjects;
using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Integrations.Gmail;

public class GmailUser : Entity<GmailUserId>
{
    public GmailUser(
        GmailUserId id,
        string givenName,
        string familyName,
        string displayName,
        string email,
        Uri avatarUri)
        : base(id)
    {
        GivenName = givenName;
        FamilyName = familyName;
        Email = email;
        DisplayName = displayName;
        AvatarUri = avatarUri;
    }

    public string GivenName { get; private set; }
    public string FamilyName { get; private set; }
    public string DisplayName { get; private set; }
    public string Email { get; private set; }
    public Uri AvatarUri { get; private set; }
}
