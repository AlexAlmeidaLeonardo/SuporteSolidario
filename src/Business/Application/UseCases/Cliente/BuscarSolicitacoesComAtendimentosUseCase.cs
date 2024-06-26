using SuporteSolidarioBusiness.Application.DTOs;
using SuporteSolidarioBusiness.Application.Repositories;

namespace SuporteSolidarioBusiness.Application.UseCases;

public class BuscarSolicitacoesComAtendimentosPorClienteUseCase
{
    private readonly ISolicitacaoRepository _solicitacaoRepository;
    private readonly long _idCliente;

    public BuscarSolicitacoesComAtendimentosPorClienteUseCase(ISolicitacaoRepository solicitacaoRepository, long idCliente)
    {
        _solicitacaoRepository = solicitacaoRepository;
        _idCliente = idCliente;
    }

    public List<SolicitacaoEmAndamentoDTO> Execute()
    {
        return _solicitacaoRepository.BuscarSolicitacoesComAtendimentosByCliente(_idCliente);
    }
}