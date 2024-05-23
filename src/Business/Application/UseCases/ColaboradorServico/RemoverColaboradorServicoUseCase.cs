using SuporteSolidarioBusiness.Application.Repositories;

namespace SuporteSolidarioBusiness.Application.UseCases;

public class RemoverColaboradorServicoUseCase
{
    private readonly IColaboradorServicoRepository _repo;
    private readonly long _id;

    public RemoverColaboradorServicoUseCase(IColaboradorServicoRepository repo, long id)
    {
        _repo = repo;
        this._id = id;
    }

    public void Execute()
    {
        _repo.Remover(_id);
    }
}