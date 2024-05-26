using SuporteSolidarioBusiness.Application.DTOs;
using SuporteSolidarioBusiness.Application.Repositories;

public class BuscarClientePorAtendimentoUseCase
{
    private IClienteRepository _clienteRepository;
    private long _idAtendimento;

    public BuscarClientePorAtendimentoUseCase(IClienteRepository clienteRepository, long idAtendimento)
    {
        _clienteRepository = clienteRepository;
        _idAtendimento = idAtendimento;
    }

    public ClienteDTO Execute()
    {
        return _clienteRepository.BuscarPorAtendimento(_idAtendimento);
    }
}