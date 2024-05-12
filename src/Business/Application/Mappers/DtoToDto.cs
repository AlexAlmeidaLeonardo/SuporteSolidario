namespace SuporteSolidarioBusiness.Application.Mappers;

using SuporteSolidarioBusiness.Domain.Entities;

public static class DtoToDto
{
    internal static ClienteEntity MapCliente(ClienteEntity entityModel, ClienteEntity newModel)
    {
        entityModel.Id = newModel.Id;
        entityModel.Ativo = newModel.Ativo;
        entityModel.Bairro = newModel.Bairro;
        entityModel.Celular = newModel.Celular;
        entityModel.Cep = newModel.Cep;
        entityModel.Cidade = newModel.Cidade;
        entityModel.Cpf = newModel.Cpf;
        entityModel.DddCelular = newModel.DddCelular;
        entityModel.DddTelefone = newModel.DddTelefone;
        entityModel.Email = newModel.Email;
        entityModel.Endereco = newModel.Endereco;
        entityModel.Estado = newModel.Estado;
        entityModel.Logradouro = newModel.Logradouro;
        entityModel.Nome = newModel.Nome;
        entityModel.Numero = newModel.Numero;
        entityModel.Sobrenome = newModel.Sobrenome;
        entityModel.Telefone = newModel.Telefone;

        return entityModel;
    }

    internal static ColaboradorEntity MapColaborador(ColaboradorEntity entityModel, ColaboradorEntity newModel)
    {
        entityModel.Id = newModel.Id;
        entityModel.Ativo = newModel.Ativo;
        entityModel.Bairro = newModel.Bairro;
        entityModel.Celular = newModel.Celular;
        entityModel.Cep = newModel.Cep;
        entityModel.Cidade = newModel.Cidade;
        entityModel.Cpf = newModel.Cpf;
        entityModel.DddCelular = newModel.DddCelular;
        entityModel.DddTelefone = newModel.DddTelefone;
        entityModel.Email = newModel.Email;
        entityModel.Endereco = newModel.Endereco;
        entityModel.Estado = newModel.Estado;
        entityModel.Logradouro = newModel.Logradouro;
        entityModel.Nome = newModel.Nome;
        entityModel.Numero = newModel.Numero;
        entityModel.Sobrenome = newModel.Sobrenome;
        entityModel.Telefone = newModel.Telefone;

        return entityModel;
    }
}