using SuporteSolidario.ViewModel;
using SuporteSolidarioBusiness.Domain.Entities;

public static class EntityToViewModel
{
    internal static ClienteViewModel MapCliente(ClienteEntity entity)
    {
        ClienteViewModel viewModel = new ClienteViewModel
        {
            Id = entity.Id,
            IdUsuario = entity.IdUsuario,
            Bairro = entity.Bairro,
            Celular = entity.Celular,
            Cep = entity.Cep,
            Cidade = entity.Cidade,
            Cpf = entity.Cpf,
            DddCelular = entity.DddCelular,
            DddTelefone = entity.DddTelefone,
            Email = entity.Email,
            Endereco = entity.Endereco,
            Estado = entity.Estado,
            Logradouro = entity.Logradouro,
            Nome = entity.Nome,
            Numero = entity.Numero,
            Sobrenome = entity.Sobrenome,
            Telefone = entity.Telefone
        };

        return viewModel;
    }

    internal static ColaboradorViewModel MapColaborador(ColaboradorEntity entity)
    {
        ColaboradorViewModel viewModel = new ColaboradorViewModel
        {
            Id = entity.Id,
            IdUsuario = entity.IdUsuario,
            Bairro = entity.Bairro,
            Celular = entity.Celular,
            Cep = entity.Cep,
            Cidade = entity.Cidade,
            Cpf = entity.Cpf,
            DddCelular = entity.DddCelular,
            DddTelefone = entity.DddTelefone,
            Email = entity.Email,
            Endereco = entity.Endereco,
            Estado = entity.Estado,
            Logradouro = entity.Logradouro,
            Nome = entity.Nome,
            Numero = entity.Numero,
            Sobrenome = entity.Sobrenome,
            Telefone = entity.Telefone
        };

        return viewModel;
    }
}