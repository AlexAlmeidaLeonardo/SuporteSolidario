using SuporteSolidarioBusiness.Application.DTOs;
using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Domain.Entities;
using SuporteSolidarioBusiness.Infrastructure.Mappers;
using SuporteSolidarioBusiness.Infrastructure.MySQL.Repositories;

public class UsuarioMySqlRepository : IUsuarioRepository
{
    private readonly SuporteSolidarioDbContext _context;

    public UsuarioMySqlRepository(SuporteSolidarioDbContext context)
    {
        _context = context;
    }

    public void Adicionar(UsuarioEntity entity, HashSaltDTO hashSalt)
    {
        UsuarioModel model = EntityToModel.MapUsuario(entity);
        model.Hash = hashSalt.Hash;
        model.Salt = hashSalt.Salt;
        model.Criacao = DateTime.Now;
        model.Alteracao = model.Criacao;

        _context.Usuarios.Add(model);

        _context.SaveChanges();
    }

    public void Atualizar(LoginDTO dto, HashSaltDTO hashSalt)
    {
        UsuarioModel model = _context.Usuarios.Where(x => x.Login == dto.Login).First();

        model.Hash = hashSalt.Hash;
        model.Salt = hashSalt.Salt;
        model.Alteracao = DateTime.Now;

        _context.Usuarios.Update(model);

        _context.SaveChanges();
    }

    public bool ExistemUsuarios()
    {
        bool existe = _context.Usuarios.Any();
        return existe;
    }

    public bool LoginExiste(string login)
    {
        bool jaExiste = _context.Usuarios.Where(x => x.Login == login).Any();
        return jaExiste;
    }
}