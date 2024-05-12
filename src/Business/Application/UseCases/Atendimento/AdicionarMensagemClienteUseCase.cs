using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Domain.Entities;

namespace SuporteSolidarioBusiness.Application.UseCases;

public class AdicionarMensagemClienteUseCase
{
    private readonly IAtendimentoMensagemRepository _repo;
    private readonly AtendimentoMensagemEntity _input;

    public AdicionarMensagemClienteUseCase(IAtendimentoMensagemRepository repo, AtendimentoMensagemEntity input)
    {
        _repo = repo;
        _input = input;
    }

    public AtendimentoMensagemEntity Execute()
    {
        AtendimentoMensagemEntity entity = _repo.MensagemCliente(_input);
        return entity;
    }
}