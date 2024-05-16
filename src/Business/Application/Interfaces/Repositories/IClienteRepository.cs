namespace SuporteSolidarioBusiness.Application.Repositories;

using SuporteSolidarioBusiness.Domain.Entities;

public interface IClienteRepository
{
    ClienteEntity Ler(long id);
    ClienteEntity LerByUsuario(long id);
    ClienteEntity Adicionar(ClienteEntity obj);
    ClienteEntity Atualizar(ClienteEntity obj);
    bool ExisteLogin(long id);
}
