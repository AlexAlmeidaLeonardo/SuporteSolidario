using SuporteSolidarioBusiness.Application.DTOs;
using SuporteSolidarioBusiness.Application.Repositories;

public class BuscarMensagensUseCase
{
    private readonly IAtendimentoMensagemRepository _atendimentoMensagemRepository;
    private readonly long _idAtendimento;

    public BuscarMensagensUseCase(IAtendimentoMensagemRepository atendimentoMensagemRepository, long idAtendimento)
    {
        _atendimentoMensagemRepository = atendimentoMensagemRepository;
        _idAtendimento = idAtendimento;
    }

    public List<AtendimentoMensagemDTO> Execute()
    {
        return _atendimentoMensagemRepository.GetMensagens(_idAtendimento);
    }
}