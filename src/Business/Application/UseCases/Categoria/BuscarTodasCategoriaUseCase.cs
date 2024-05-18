namespace SuporteSolidarioBusiness.Application.UseCases;

using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Domain.Entities;

public class BuscarTodasCategoriaUseCase
{
    private readonly ICategoriaRepository _repo;

    public BuscarTodasCategoriaUseCase(ICategoriaRepository repo)
    {
        this._repo = repo;
    }

    public IEnumerable<CategoriaEntity> Execute()
    {
        return _repo.LerTodos();
    }
}