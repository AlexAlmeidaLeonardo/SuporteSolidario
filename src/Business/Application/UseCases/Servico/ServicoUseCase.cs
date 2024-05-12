namespace SuporteSolidarioBusiness.Application.UseCases;

using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Domain.Entities;

public class BuscarServicoUseCase
{
    private readonly IServicoRepository _repo;
    private readonly long _id;

    public BuscarServicoUseCase(IServicoRepository repo, long id)
    {
        this._repo = repo;
        this._id = id;
    }

    public ServicoEntity Execute()
    {
        return _repo.Ler(_id);
    }
}