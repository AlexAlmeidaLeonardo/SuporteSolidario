namespace SuporteSolidarioBusiness.Application.UseCases;

using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Domain.Entities;

public class BuscarCategoriaUseCase
{
    private readonly ICategoriaRepository _repo;
    private readonly long _id;

    public BuscarCategoriaUseCase(ICategoriaRepository repo, long id)
    {
        this._repo = repo;
        this._id = id;
    }

    public CategoriaEntity Execute()
    {
        return _repo.Ler(_id);
    }
}