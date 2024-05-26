using SuporteSolidarioBusiness.Application.DTOs;
using SuporteSolidarioBusiness.Application.Repositories;

namespace SuporteSolidarioBusiness.Application.UseCases;

public class BuscarSolicitacoesComAtendimentosByColaborador
{
    private readonly ISolicitacaoRepository _solicitacaoRepository;
    private readonly long _idColaborador;

    public BuscarSolicitacoesComAtendimentosByColaborador(ISolicitacaoRepository solicitacaoRepository, long idColaborador)
    {
        _solicitacaoRepository = solicitacaoRepository;
        _idColaborador = idColaborador;
    }

    public List<SolicitacaoEmAndamentoDTO> Execute()
    {
        return _solicitacaoRepository.BuscarSolicitacoesComAtendimentosByCliente(_idColaborador);
    }
}