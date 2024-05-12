using SuporteSolidarioBusiness.Infrastructure.MySQL;

namespace SuporteSolidarioBusiness.Infrastructure.Mappers;

public static class ModelToModel
{
    internal static ColaboradorModel MapColaborador(ColaboradorModel entityModel, ColaboradorModel newModel)
    {
        entityModel.Id = newModel.Id;
        entityModel.IdUsuario = newModel.IdUsuario;
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