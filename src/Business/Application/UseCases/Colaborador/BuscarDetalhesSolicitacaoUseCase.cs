using SuporteSolidarioBusiness.Application.DTOs;
using SuporteSolidarioBusiness.Application.Repositories;

public class BuscarDetalhesSolicitacaoUseCase
{
    private readonly IColaboradorRepository _colaboradorRepository;
    private readonly long _idSolicitacao;

    public BuscarDetalhesSolicitacaoUseCase(IColaboradorRepository colaboradorRepository, long idSolicitacao)
    {
        _colaboradorRepository = colaboradorRepository;
        _idSolicitacao = idSolicitacao;
    }

    public DetalhesSolicitacaoDTO Execute()
    {
        DetalhesSolicitacaoDTO retorno = _colaboradorRepository.GetDetalhesSolicitacao(_idSolicitacao);

        return retorno;
    }
}