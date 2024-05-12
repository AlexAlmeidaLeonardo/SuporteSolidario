namespace SuporteSolidarioBusiness.Application.UseCases;

using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Domain.Entities;

public class AdicionarColaboradorServicoUseCase
{
    private readonly IColaboradorServicoRepository _repo;
    private readonly ColaboradorServicoEntity _input;

    public AdicionarColaboradorServicoUseCase(IColaboradorServicoRepository repo, ColaboradorServicoEntity input)
    {
        _repo = repo;
        _input = input;
    }

    public ColaboradorServicoEntity Execute()
    {
        return _repo.Adicionar(_input);
    }
}