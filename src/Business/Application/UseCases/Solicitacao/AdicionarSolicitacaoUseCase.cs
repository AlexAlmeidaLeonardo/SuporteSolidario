namespace SuporteSolidarioBusiness.Application.UseCases;

using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Domain.Entities;

public class AdicionarSolicitacaoUseCase
{
    private readonly ISolicitacaoRepository _repo;
    private readonly IClienteRepository _clienteRepository;
    private readonly SolicitacaoEntity _input;

    public AdicionarSolicitacaoUseCase(ISolicitacaoRepository repo, IClienteRepository clienteRepository, SolicitacaoEntity input)
    {
        _repo = repo;
        this._clienteRepository = clienteRepository;
        _input = input;
    }

    public SolicitacaoEntity Execute()
    {
        ClienteEntity cliente = _clienteRepository.Ler(_input.IdCliente);
        if(cliente == null)
        {
            throw new NotFoundException($"Cliente {_input.IdCliente} não encontrado.");
        }

        // Verifica se o cliente já possui uma solicitação aberta com o mesmo serviço
        SolicitacaoModel solicitacao = _repo.GetSolicitacaoAberta(_input.IdCliente, _input.IdServico);
        if(solicitacao != null)
        {
            throw new Exception($"Cliente {_input.IdCliente} já possui uma solicitação aberta.");
        }

        // Congela na solicitação o endereço atual do cliente
        _input.Bairro = cliente.Bairro;
        _input.Cidade = cliente.Cidade;
        _input.Estado = cliente.Estado;
        _input.Endereco = cliente.Endereco;
        _input.Numero = cliente.Numero;
        _input.Cep = cliente.Cep;
        _input.Logradouro = cliente.Logradouro;
        _input.Latitude = cliente.Latitude;
        _input.Longitude = cliente.Longitude;

        // Status inicial da solicitação
        _input.Data = DateTime.Now;
        _input.EmAberto = true;

        return _repo.Adicionar(_input);
    }
}