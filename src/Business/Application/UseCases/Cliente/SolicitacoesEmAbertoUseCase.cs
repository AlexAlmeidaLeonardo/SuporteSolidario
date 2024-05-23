namespace SuporteSolidarioBusiness.Application.UseCases;

using SuporteSolidarioBusiness.Application.DTOs;
using SuporteSolidarioBusiness.Application.Repositories;

public class SolicitacoesEmAbertoUseCase
{
    private readonly ISolicitacaoRepository _solicitacaoRepository;
    private readonly long _idCliente;
    public SolicitacoesEmAbertoUseCase(ISolicitacaoRepository solicitacaoRepository, long idCliente)
    {
        _solicitacaoRepository = solicitacaoRepository;
        _idCliente = idCliente;
    }

    public IEnumerable<SolicitacaoEmAbertoDTO> Execute()
    {
        return _solicitacaoRepository.GetSolicitacoesEmAberto(_idCliente);
    }
}