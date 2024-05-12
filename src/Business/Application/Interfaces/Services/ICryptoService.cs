namespace SuporteSolidarioBusiness.Application.Services;

using SuporteSolidarioBusiness.Application.DTOs;

public interface ICryptoService
{
    HashSaltDTO Crypt(string password);
    bool VerificaPassword(string password, string hash, string saltString);
}