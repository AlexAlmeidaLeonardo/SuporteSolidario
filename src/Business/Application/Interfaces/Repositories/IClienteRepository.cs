namespace SuporteSolidarioBusiness.Application.Repositories;

using SuporteSolidarioBusiness.Application.DTOs;
using SuporteSolidarioBusiness.Domain.Entities;

public interface IClienteRepository
{
    ClienteEntity Ler(long id);
    ClienteEntity LerByUsuario(long id);
    ClienteEntity Adicionar(ClienteEntity obj);
    ClienteEntity Atualizar(ClienteEntity obj);
    ClienteDTO BuscarPorAtendimento(long idAtendimento);

    bool ExisteLogin(long id);
}
