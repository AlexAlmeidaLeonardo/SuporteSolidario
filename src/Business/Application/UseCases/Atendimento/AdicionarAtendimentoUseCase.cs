using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Domain.Entities;

namespace SuporteSolidarioBusiness.Application.UseCases;

public class AdicionarAtendimentoUseCase
{
    private readonly IAtendimentoRepository _repo;
    private readonly AtendimentoEntity _input;

    public AdicionarAtendimentoUseCase(IAtendimentoRepository repo, AtendimentoEntity input)
    {
        _repo = repo;
        _input = input;
    }

    public AtendimentoEntity Execute()
    {
        //_input.AnunciadoEm = DateTime.Now;
        _input.AtendidoEm = DateTime.Now;
        _input.ConfirmadoEm = null;        
        _input.FinalizadoEm = null;
        _input.Avaliacao = null;

        AtendimentoEntity entity = _repo.Adicionar(_input);
        return entity;
    }
}