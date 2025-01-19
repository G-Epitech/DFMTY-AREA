using Zeus.BuildingBlocks.Domain.Models;
using Zeus.Daemon.Domain.Providers.Github.ValueObjects;

namespace Zeus.Daemon.Domain.Providers.Github;

public class GithubPullRequest : Entity<GithubPullRequestId>
{
    public GithubPullRequest(GithubPullRequestId id, Uri uri, string title, int number, string? body,
        string authorName) : base(id)
    {
        Uri = uri;
        Title = title;
        Number = number;
        Body = body;
        AuthorName = authorName;
    }

    public Uri Uri { get; private set; }
    public string Title { get; private set; }
    public int Number { get; private set; }
    public string? Body { get; private set; }
    public string AuthorName { get; private set; }
}
