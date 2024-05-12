namespace SuporteSolidarioBusiness.Application.UseCases;

using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Domain.Entities;

public class BuscarColaboradorServicoUseCase
{

    private readonly IColaboradorServicoRepository _repo;
    private readonly long _id;

    public BuscarColaboradorServicoUseCase(IColaboradorServicoRepository repo, long id)
    {
        _repo = repo;
        _id = id;
    }

    public ColaboradorServicoEntity Execute()
    {
        ColaboradorServicoEntity entity = _repo.Ler(_id);

        return entity;
    }
}