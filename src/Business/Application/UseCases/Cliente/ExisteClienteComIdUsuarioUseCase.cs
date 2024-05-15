namespace SuporteSolidarioBusiness.Application.UseCases;

using SuporteSolidarioBusiness.Application.Repositories;

public class ExisteClienteComIdUsuarioUseCase
{
    private readonly IClienteRepository _clienteRepository;
    private readonly long _idUsuario;
    public ExisteClienteComIdUsuarioUseCase(IClienteRepository clienteRepository, long idUsuario)
    {
        _clienteRepository = clienteRepository;
        _idUsuario = idUsuario;
    }

    public bool Execute()
    {
        return _clienteRepository.ExisteLogin(_idUsuario);
    }
}