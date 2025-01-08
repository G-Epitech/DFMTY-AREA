using System.Security.Cryptography;
using System.Text;

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
        var salt = RandomNumberGenerator.GetBytes(512 / 8);
        var pbkdf2 =
            Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), salt, 10000, HashAlgorithmName.SHA512, 512 / 8);

        return new Password(Convert.ToBase64String(pbkdf2));
    }

    public bool Verify(string password)
    {
        var pbkdf2 = Convert.FromBase64String(Hash);
        var salt = pbkdf2.Take(512 / 8).ToArray();
        var hash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), salt, 10000, HashAlgorithmName.SHA512,
            512 / 8);

        return pbkdf2.SequenceEqual(hash);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Hash;
    }
}
