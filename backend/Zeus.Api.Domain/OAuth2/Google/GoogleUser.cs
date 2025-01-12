using Zeus.Api.Domain.OAuth2.Google.ValueObjects;
using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.OAuth2.Google;

public sealed class GoogleUser : Entity<GoogleUserId>
{
    public string Email { get; private set; }
    public string Name { get; private set; }
    public string GivenName { get; private set; }
    public string FamilyName { get; private set; }
    public Uri Picture { get; private set; }

    private GoogleUser(
        GoogleUserId id,
        string email,
        string name,
        string givenName,
        string familyName,
        Uri picture) : base(id)
    {
        Email = email;
        Name = name;
        GivenName = givenName;
        FamilyName = familyName;
        Picture = picture;
    }

    public static GoogleUser Create(
        GoogleUserId id,
        string email,
        string name,
        string givenName,
        string familyName,
        Uri picture)
    {
        return new GoogleUser(id, email, name, givenName, familyName, picture);
    }
}
