using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Authentication.ValueObjects;

public sealed class Password : ValueObject
{
    public Password(string hash)
    {
        Hash = hash;
    }

    public string Hash { get; }

    public static Password Create(string password)
    {
        var hash = BCrypt.Net.BCrypt.HashPassword(password);

        return new Password(hash);
    }

    public bool Verify(string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, Hash);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Hash;
    }
}
