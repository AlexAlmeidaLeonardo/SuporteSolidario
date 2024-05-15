namespace SuporteSolidarioBusiness.Application.UseCases;

using SuporteSolidarioBusiness.Application.Repositories;

public class HistoricoClienteUseCase
{
    private readonly ISolicitacaoRepository _solicitacaoRepository;
    private readonly long _idCliente;

    public HistoricoClienteUseCase(ISolicitacaoRepository solicitacaoRepository, long idCliente)
    {
        _solicitacaoRepository = solicitacaoRepository;
        _idCliente = idCliente;
    }

    public IEnumerable<SolicitacaoModel> Execute()
    {
        return _solicitacaoRepository.GetHistoricoByCliente(_idCliente);
    }
}