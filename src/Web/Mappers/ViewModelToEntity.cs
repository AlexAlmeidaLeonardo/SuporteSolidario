using SuporteSolidario.ViewModel;
using SuporteSolidarioBusiness.Domain.Entities;

public static class ViewModelToEntity
{
    internal static ClienteEntity MapCliente(ClienteViewModel viewModel)
    {
        ClienteEntity entity = new ClienteEntity
        {
            Id = viewModel.Id,
            IdUsuario = viewModel.IdUsuario,
            Bairro = viewModel.Bairro,
            Celular = viewModel.Celular,
            Cep = viewModel.Cep,
            Cidade = viewModel.Cidade,
            Cpf = viewModel.Cpf,
            DddCelular = viewModel.DddCelular,
            DddTelefone = viewModel.DddTelefone,
            Email = viewModel.Email,
            Endereco = viewModel.Endereco,
            Estado = viewModel.Estado,
            Logradouro = viewModel.Logradouro,
            Nome = viewModel.Nome,
            Numero = viewModel.Numero,
            Sobrenome = viewModel.Sobrenome,
            Telefone = viewModel.Telefone
        };
        return entity;
    }

    internal static ColaboradorEntity MapColaborador(ColaboradorViewModel viewModel)
    {
        ColaboradorEntity entity = new ColaboradorEntity
        {
            Id = viewModel.Id,
            IdUsuario = viewModel.IdUsuario,
            Bairro = viewModel.Bairro,
            Celular = viewModel.Celular,
            Cep = viewModel.Cep,
            Cidade = viewModel.Cidade,
            Cpf = viewModel.Cpf,
            DddCelular = viewModel.DddCelular,
            DddTelefone = viewModel.DddTelefone,
            Email = viewModel.Email,
            Endereco = viewModel.Endereco,
            Estado = viewModel.Estado,
            Logradouro = viewModel.Logradouro,
            Nome = viewModel.Nome,
            Numero = viewModel.Numero,
            Sobrenome = viewModel.Sobrenome,
            Telefone = viewModel.Telefone
        };
        return entity;
    }
}