namespace SuporteSolidarioBusiness.Application.UseCases;

using SuporteSolidarioBusiness.Domain.Entities;
using SuporteSolidarioBusiness.Application.Repositories;

public class BuscarColaboradorByUsuarioUseCase
{
    private readonly IColaboradorRepository _repo;
    private readonly long _id;

    public BuscarColaboradorByUsuarioUseCase(IColaboradorRepository repo, long id)
    {
        _repo = repo;
        _id = id;
    }

    public ColaboradorEntity Execute()
    {
        ColaboradorEntity entity = _repo.LerByUsuario(_id);
        if(entity is null)
        {
            return null;
            //throw new NotFoundException($"Colaborador com id {_id} não encontrado");
        }

        return entity;
    }
}