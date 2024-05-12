using SuporteSolidarioBusiness.Application.DTOs;
using SuporteSolidarioBusiness.Domain.Entities;

namespace SuporteSolidarioBusiness.Application.Repositories;

public interface IAuthRepository
{
    HashSaltDTO GetSalt(LoginDTO dto);
    UsuarioEntity GetUserByLogin(string login);
    long GetUserId(string login);
}