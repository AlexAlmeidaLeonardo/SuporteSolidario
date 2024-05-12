namespace SuporteSolidarioBusiness.Application.UseCases;

using SuporteSolidarioBusiness.Domain.Entities;
using SuporteSolidarioBusiness.Application.Repositories;

public class BuscarColaboradorUseCase
{
    private readonly IColaboradorRepository _repo;
    private readonly long _id;

    public BuscarColaboradorUseCase(IColaboradorRepository repo, long id)
    {
        _repo = repo;
        _id = id;
    }

    public ColaboradorEntity Execute()
    {
        ColaboradorEntity entity = _repo.Ler(_id);
        if(entity is null)
        {
            throw new NotFoundException($"Colaborador com id {_id} não encontrado");
        }

        return entity;
    }
}