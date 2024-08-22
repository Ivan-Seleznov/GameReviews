using System.Security.Cryptography;
using GameReviews.Infrastructure.Constants;
using GameReviews.Application.Common.Interfaces.Authentication;

namespace GameReviews.Infrastructure.Authentication;
public class PasswordHasher : IPasswordHasher
{
    public readonly HashAlgorithmName AlgorithmName = HashAlgorithmName.SHA512;

    public string Hash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(PasswordHasherConstants.SaltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, PasswordHasherConstants.Iterations, AlgorithmName,
            PasswordHasherConstants.HashSize);

        return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
    }

    public bool Verify(string password, string passwordHash)
    {
        string[] hashParts = passwordHash.Split('-');
        if (hashParts.Length != 2)
        {
            return false;
        }

        byte[] hash = Convert.FromHexString(hashParts[0]);
        byte[] salt = Convert.FromHexString(hashParts[1]);

        byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, PasswordHasherConstants.Iterations, AlgorithmName,
            PasswordHasherConstants.HashSize);

        return CryptographicOperations.FixedTimeEquals(hash, inputHash);
    }   
}
