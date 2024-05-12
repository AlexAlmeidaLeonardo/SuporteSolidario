using SuporteSolidarioBusiness.Domain.Entities;

namespace SuporteSolidarioBusiness.Application.Services;

public interface ITokenService
{
    string GetToken(UsuarioEntity usuario);
    string GetLoggedUser(string token);
    UsuarioEntity GetUsuarioDTO(string tokenAtual);
}