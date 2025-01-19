using Zeus.Api.Domain.Integrations.Github.ValueObjects;
using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Integrations.Github;

public class GithubUser : Entity<GithubUserId>
{
    public string Login { get; private set; }
    public Uri AvatarUri { get; private set; }
    public Uri ProfileUri { get; private set; }
    public string Name { get; private set; }
    public string? Company { get; private set; }
    public string? Blog { get; private set; }
    public string? Location { get; private set; }
    public string? Email { get; private set; }
    public string? Bio { get; private set; }
    public int Followers { get; private set; }
    public int Following { get; private set; }

    public GithubUser(GithubUserId id,
        string login,
        Uri avatarUri,
        Uri profileUri,
        string name,
        string? company,
        string? blog,
        string? location,
        string? email,
        string? bio,
        int followers,
        int following) : base(id)
    {
        Login = login;
        AvatarUri = avatarUri;
        ProfileUri = profileUri;
        Name = name;
        Company = company;
        Blog = blog;
        Location = location;
        Email = email;
        Bio = bio;
        Followers = followers;
        Following = following;
    }
}
