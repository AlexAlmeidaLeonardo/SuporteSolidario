namespace SuporteSolidarioBusiness.Application.UseCases;

using SuporteSolidarioBusiness.Domain.Entities;
using SuporteSolidarioBusiness.Application.Repositories;

public class BuscarClienteByUsuarioUseCase
{
    private readonly IClienteRepository _repo;
    private readonly long _id;

    public BuscarClienteByUsuarioUseCase(IClienteRepository repo, long id)
    {
        _repo = repo;
        _id = id;
    }

    public ClienteEntity Execute()
    {
        ClienteEntity entity = _repo.LerByUsuario(_id);
        if(entity is null)
        {
            throw new NotFoundException($"Cliente com id {_id} não encontrado");
        }

        return entity;
    }
}