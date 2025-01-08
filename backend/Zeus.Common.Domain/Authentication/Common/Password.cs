using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Common.Domain.Authentication.Common;

public sealed class Password : ValueObject
{
    public string Hash { get; }

    public Password(string hash)
    {
        Hash = hash;
    }

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
