namespace SuporteSolidarioBusiness.Application.UseCases;

using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Domain.Entities;

public class BuscarSolicitacaoUseCase
{
    private readonly ISolicitacaoRepository _repo;
    private readonly long _id;

    public BuscarSolicitacaoUseCase(ISolicitacaoRepository repo, long id)
    {
        _repo = repo;
        _id = id;
    }

    public SolicitacaoEntity Execute()
    {
        SolicitacaoEntity entity = _repo.Ler(_id);

        return entity;
    }

}