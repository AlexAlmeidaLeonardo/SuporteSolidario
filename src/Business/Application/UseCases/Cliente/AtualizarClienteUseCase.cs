namespace SuporteSolidarioBusiness.Application.UseCases;

using SuporteSolidarioBusiness.Domain.Entities;
using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Application.Mappers;

public class AtualizarClienteUseCase
{
    private readonly IClienteRepository _repo;
    private readonly ClienteEntity _input;

    public AtualizarClienteUseCase(IClienteRepository repo, ClienteEntity input)
    {
        _repo = repo;
        _input = input;
    }

    public ClienteEntity Execute()
    {
        ClienteEntity dtoOriginal = _repo.Ler(_input.Id);
        if(dtoOriginal is null)
        {
            throw new NotFoundException($"Cliente com id {_input.Id} não encontrado");
        }

        // Verifica se houve alteração no endereço
        // Se sim, marcar registro como pendente de coordenadas GPS
        if(dtoOriginal.Logradouro != _input.Logradouro || 
           dtoOriginal.Endereco != _input.Endereco || 
           dtoOriginal.Numero != _input.Numero || 
           dtoOriginal.Bairro != _input.Bairro || 
           dtoOriginal.Cidade != _input.Cidade || 
           dtoOriginal.Estado != _input.Estado || 
           dtoOriginal.Cep != _input.Cep)
        {
            // Marcar registro como pendente de coordenadas GPS
            _input.PossuiCoordenadasGps = false;
        }

        dtoOriginal = DtoToDto.MapCliente(dtoOriginal, _input);
        dtoOriginal.Alteracao = DateTime.Now;
        return _repo.Atualizar(dtoOriginal);
    }
}