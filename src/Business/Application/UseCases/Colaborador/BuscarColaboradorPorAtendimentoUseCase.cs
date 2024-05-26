using SuporteSolidarioBusiness.Application.DTOs;
using SuporteSolidarioBusiness.Application.Repositories;

public class BuscarColaboradorPorAtendimentoUseCase
{
    private IColaboradorRepository _colaboradorRepository;
    private long _idAtendimento;

    public BuscarColaboradorPorAtendimentoUseCase(IColaboradorRepository colaboradorRepository, long idAtendimento)
    {
        _colaboradorRepository = colaboradorRepository;
        _idAtendimento = idAtendimento;
    }

    public ColaboradorDTO Execute()
    {
        return _colaboradorRepository.BuscarPorAtendimento(_idAtendimento);
    }
}