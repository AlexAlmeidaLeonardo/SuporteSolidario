namespace SuporteSolidarioBusiness.Application.UseCases;

using SuporteSolidarioBusiness.Application.Repositories;

public class ExisteColaboradorComIdUsuarioUseCase
{
    private readonly IColaboradorRepository _colaboradorRepository;
    private readonly long _idUsuario;
    public ExisteColaboradorComIdUsuarioUseCase(IColaboradorRepository colaboradorRepository, long idUsuario)
    {
        _colaboradorRepository = colaboradorRepository;
        _idUsuario = idUsuario;
    }

    public bool Execute()
    {
        return _colaboradorRepository.ExisteLogin(_idUsuario);
    }
}