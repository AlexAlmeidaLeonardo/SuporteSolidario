namespace SuporteSolidarioBusiness.Application.UseCases;

using SuporteSolidarioBusiness.Domain.Entities;
using SuporteSolidarioBusiness.Application.Repositories;

public class BuscarClienteUseCase
{
    private readonly IClienteRepository _repo;
    private readonly long _id;

    public BuscarClienteUseCase(IClienteRepository repo, long id)
    {
        _repo = repo;
        _id = id;
    }

    public ClienteEntity Execute()
    {
        ClienteEntity entity = _repo.Ler(_id);
        if(entity is null)
        {
            throw new NotFoundException($"Cliente com id {_id} não encontrado");
        }

        return entity;
    }
}