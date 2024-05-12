using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Domain.Entities;

namespace SuporteSolidarioBusiness.Application.UseCases;

public class AdicionarMensagemColaboradorUseCase
{
    private readonly IAtendimentoMensagemRepository _repo;
    private readonly AtendimentoMensagemEntity _input;

    public AdicionarMensagemColaboradorUseCase(IAtendimentoMensagemRepository repo, AtendimentoMensagemEntity input)
    {
        _repo = repo;
        _input = input;
    }

    public AtendimentoMensagemEntity Execute()
    {
        AtendimentoMensagemEntity entity = _repo.MensagemColaborador(_input);
        return entity;
    }
}