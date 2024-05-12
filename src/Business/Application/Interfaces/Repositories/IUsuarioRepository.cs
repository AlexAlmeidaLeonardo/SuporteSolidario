using SuporteSolidarioBusiness.Application.DTOs;
using SuporteSolidarioBusiness.Domain.Entities;

namespace SuporteSolidarioBusiness.Application.Repositories;

public interface IUsuarioRepository
{
    void Adicionar(UsuarioEntity entity, HashSaltDTO hashSalt);
    void Atualizar(LoginDTO dto, HashSaltDTO hashSalt);
    bool LoginExiste(string login);
    bool ExistemUsuarios();
}