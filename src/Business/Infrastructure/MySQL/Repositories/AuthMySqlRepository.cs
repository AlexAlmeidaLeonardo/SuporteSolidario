using SuporteSolidarioBusiness.Application.DTOs;
using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Domain.Entities;
using SuporteSolidarioBusiness.Infrastructure.Mappers;
using SuporteSolidarioBusiness.Infrastructure.MySQL.Repositories;

public class AuthMySqlRepository : IAuthRepository
{
    private readonly SuporteSolidarioDbContext _context;

    public AuthMySqlRepository(SuporteSolidarioDbContext context)
    {
        _context = context;
    }

    public HashSaltDTO GetSalt(LoginDTO entity)
    {
        HashSaltDTO? output = _context.Usuarios
                                .Where(u => u.Login == entity.Login && u.Ativo )
                                .Select(h => new HashSaltDTO
                                {
                                    Hash = h.Hash,
                                    Salt = h.Salt,
                                }).FirstOrDefault();

        return output;
    }

    public UsuarioEntity GetUserByLogin(string login)
    {
        UsuarioModel model = _context.Usuarios
                                .Where(u => u.Login == login)
                                .First();

        UsuarioEntity entity = ModelToEntity.MapUsuario(model);

        return entity;
    }

    public long GetUserId(string login)
    {
        long userId = _context.Usuarios
                        .Where(u => u.Login == login)
                        .Select(i => i.Id)
                        .FirstOrDefault();

        return userId;
    }
}