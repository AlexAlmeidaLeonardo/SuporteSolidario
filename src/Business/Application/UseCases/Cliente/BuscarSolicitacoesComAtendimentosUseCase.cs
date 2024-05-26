using SuporteSolidarioBusiness.Application.DTOs;
using SuporteSolidarioBusiness.Application.Repositories;

namespace SuporteSolidarioBusiness.Application.UseCases;

public class BuscarSolicitacoesComAtendimentosUseCase
{
    private readonly ISolicitacaoRepository _solicitacaoRepository;
    private readonly long _idCliente;

    public BuscarSolicitacoesComAtendimentosUseCase(ISolicitacaoRepository solicitacaoRepository, long idCliente)
    {
        _solicitacaoRepository = solicitacaoRepository;
        _idCliente = idCliente;
    }

    public List<SolicitacaoEmAndamentoDTO> Execute()
    {
        return _solicitacaoRepository.BuscarSolicitacoesComAtendimentos(_idCliente);
    }
}