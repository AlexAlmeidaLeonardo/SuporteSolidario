namespace SuporteSolidarioBusiness.Application.UseCases;

using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Domain.Entities;

public class BuscarServicosPorCategoriaUseCase
{
    private readonly IServicoRepository _repo;
    private readonly long _idCategoria;

    public BuscarServicosPorCategoriaUseCase(IServicoRepository repo, long id)
    {
        this._repo = repo;
        this._idCategoria = id;
    }

    public IEnumerable<ServicoEntity> Execute()
    {
        return _repo.LerTodosPorCategoria(_idCategoria);
    }
}