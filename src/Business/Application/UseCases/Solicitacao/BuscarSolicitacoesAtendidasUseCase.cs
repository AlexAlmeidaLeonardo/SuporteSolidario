namespace SuporteSolidarioBusiness.Application.UseCases;

using SuporteSolidarioBusiness.Application.DTOs;
using SuporteSolidarioBusiness.Application.Repositories;

public class BuscarSolicitacoesAtendidasUseCase
{
    private readonly ISolicitacaoRepository _solicitacaoRepository;
    private readonly long _idCliente;

    public BuscarSolicitacoesAtendidasUseCase(ISolicitacaoRepository solicitacaoRepository, long idCliente)
    {
        _solicitacaoRepository = solicitacaoRepository;
        _idCliente = idCliente;
    }

    public List<SolicitacaoEmAbertoDTO> Execute()
    {
        //var solicitacoes = _solicitacaoRepository.GetSolicitacoesAtendidas(_idCliente);
        throw new NotImplementedException();
    }
}