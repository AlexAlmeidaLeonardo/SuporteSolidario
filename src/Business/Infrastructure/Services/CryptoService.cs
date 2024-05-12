using System.Security.Cryptography;
using System.Text;
using SuporteSolidarioBusiness.Application.DTOs;
using SuporteSolidarioBusiness.Application.Services;

public class CryptoService : ICryptoService
{
    const int keySize = 127;
    const int iterations = 350000;
    HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

    public HashSaltDTO Crypt(string password)
    {
        HashSaltDTO output = new HashSaltDTO();

        byte[] salt = RandomNumberGenerator.GetBytes(keySize);

        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            iterations,
            hashAlgorithm,
            keySize);

        output.Hash = Convert.ToHexString(hash);
        output.Salt = Convert.ToHexString(salt);

        return output;
    }

    public bool VerificaPassword(string password, string hash, string saltString)
    {
        byte[] salt = Convert.FromHexString(saltString);

        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
    }
}