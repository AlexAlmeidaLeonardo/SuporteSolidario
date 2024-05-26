using SuporteSolidarioBusiness.Application.Repositories;

namespace SuporteSolidarioBusiness.Application.UseCases;

public class RecusarAtendimentoUseCase
{
    private readonly IAtendimentoRepository _atendimentoRepository;
    private readonly long _idSolicitacao;

    public RecusarAtendimentoUseCase(IAtendimentoRepository atendimentoRepository, long idSolicitacao)
    {
        _atendimentoRepository = atendimentoRepository;
        _idSolicitacao = idSolicitacao;
    }

    public bool Execute()
    {
        bool retorno = _atendimentoRepository.SetAsRecusado(_idSolicitacao);
        return retorno;
    }
}