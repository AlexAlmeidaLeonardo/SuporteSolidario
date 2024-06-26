using SuporteSolidarioBusiness.Domain.Entities;
using SuporteSolidarioBusiness.Infrastructure.MySQL;

namespace SuporteSolidarioBusiness.Infrastructure.Mappers;

public static class EntityToModel
{
    internal static ClienteModel MapCliente(ClienteEntity entity)
    {
        ClienteModel model = new ClienteModel
        {
            Alteracao = entity.Alteracao,
            Ativo = entity.Ativo,
            Bairro = entity.Bairro,
            Celular = entity.Celular,
            Cep = entity.Cep,
            Cidade = entity.Cidade,
            Cpf = entity.Cpf,
            Criacao = entity.Criacao,
            DddCelular = entity.DddCelular,
            DddTelefone = entity.DddTelefone,
            Email = entity.Email,
            Endereco = entity.Endereco,
            Estado = entity.Estado,
            Id = entity.Id,
            Inativacao = entity.Inativacao,
            Logradouro = entity.Logradouro,
            Nome = entity.Nome,
            Numero = entity.Numero,
            Sobrenome = entity.Sobrenome,
            Telefone = entity.Telefone,
            IdUsuario = entity.IdUsuario,

            Latitude = entity.Latitude,
            Longitude = entity.Longitude,
            PossuiCoordenadasGps = entity.PossuiCoordenadasGps
        };
        
        return model;
    }

    internal static ColaboradorModel MapColaborador(ColaboradorEntity entity)
    {
        ColaboradorModel model = new ColaboradorModel
        {
            Alteracao = entity.Alteracao,
            Ativo = entity.Ativo,
            Bairro = entity.Bairro,
            Celular = entity.Celular,
            Cep = entity.Cep,
            Cidade = entity.Cidade,
            Cpf = entity.Cpf,
            Criacao = entity.Criacao,
            DddCelular = entity.DddCelular,
            DddTelefone = entity.DddTelefone,
            Email = entity.Email,
            Endereco = entity.Endereco,
            Estado = entity.Estado,
            Id = entity.Id,
            Inativacao = entity.Inativacao,
            Logradouro = entity.Logradouro,
            Nome = entity.Nome,
            Numero = entity.Numero,
            Sobrenome = entity.Sobrenome,
            Telefone = entity.Telefone,
            IdUsuario = entity.IdUsuario,

            Latitude = entity.Latitude,
            Longitude = entity.Longitude,
            PossuiCoordenadasGps = entity.PossuiCoordenadasGps
        };
        
        return model;
    }

    internal static CategoriaModel MapCategoria(CategoriaEntity entity)
    {
        CategoriaModel model = new CategoriaModel
        {
            Id = entity.Id,
            Descricao = entity.Descricao
        };
        
        return model;
    }

    internal static ServicoModel MapServico(ServicoEntity entity)
    {
        ServicoModel model = new ServicoModel
        {
            Id = entity.Id,
            Descricao = entity.Descricao
        };
        
        return model;
    }

    internal static ColaboradorServicoModel MapColaboradorServico(ColaboradorServicoEntity entity)
    {
        ColaboradorServicoModel model = new ColaboradorServicoModel
        {
            Id = entity.Id,

            Colaborador = new ColaboradorModel
            {
                Id = entity.IdColaborador
            },

            Servico = new ServicoModel
            {
                Id = entity.IdServico
            }
        };

        return model;
    }

    internal static AtendimentoMensagemModel MapAtendimentoMensagem(AtendimentoMensagemEntity entity)
    {
        AtendimentoMensagemModel model = new AtendimentoMensagemModel
        {
            Id = entity.Id,
            IdAtendimento = entity.IdAtendimento,
            Mensagem = entity.Mensagem,
            IdEmResposta = entity.IdEmResposta,
            Criacao = entity.Criacao,
            Alteracao = entity.Alteracao
        };

        return model;
    }

    internal static AtendimentoModel MapAtendimento(AtendimentoEntity entity)
    {
        AtendimentoModel model = new AtendimentoModel
        {
            Id = entity.Id,
            AtendidoEm = entity.AtendidoEm,
            Avaliacao = entity.Avaliacao,
            /*
            Colaborador = new ColaboradorModel
            {
                Id = entity.IdColaborador
            },
            */
            IdColaborador = entity.IdColaborador,
            ConfirmadoEm = entity.ConfirmadoEm,
            FinalizadoEm = entity.FinalizadoEm,
            IdSolicitacao = entity.IdSolicitacao
            /*
            Solicitacao = new SolicitacaoModel
            {
                Id = entity.IdSolicitacao
            }
            */
        };

        return model;
    }

    internal static UsuarioModel MapUsuario(UsuarioEntity entity)
    {
        UsuarioModel model = new UsuarioModel
        {
            Id = entity.Id,
            Email = entity.Email,
            Login = entity.Login,
            Celular = entity.Celular,
            TipoDeUsuario = entity.TipoDeUsuario,
            Ativo = entity.Ativo
        };

        return model;
    }

    internal static SolicitacaoModel MapSolicitacao(SolicitacaoEntity entity)
    {
        SolicitacaoModel model = new SolicitacaoModel
        {
            Id = entity.Id,
            IdCliente = entity.IdCliente,
            IdServico = entity.IdServico,
            Data = entity.Data,
            Detalhes = entity.Detalhes,
            DataServico = entity.DataServico,            
            EmAberto = entity.EmAberto,
            
            Cep = entity.Cep,
            Logradouro = entity.Logradouro,
            Endereco = entity.Endereco,
            Numero = (int)entity.Numero,
            Bairro = entity.Bairro,
            Cidade = entity.Cidade,
            Estado = entity.Estado,
            Latitude = (double)entity.Latitude,
            Longitude = (double)entity.Longitude,
        };

        return model;
    }

    internal static void MapCliente(ClienteEntity entity, ClienteModel? model)
    {
        if (model is null) 
        {
            model = new ClienteModel();
            model.Id = entity.Id;
        }

        model.Alteracao = entity.Alteracao;
        model.Ativo = entity.Ativo;
        model.Bairro = entity.Bairro;
        model.Celular = entity.Celular;
        model.Cep = entity.Cep;
        model.Cidade = entity.Cidade;
        model.Cpf = entity.Cpf;
        model.Criacao = entity.Criacao;
        model.DddCelular = entity.DddCelular;
        model.DddTelefone = entity.DddTelefone;
        model.Email = entity.Email;
        model.Endereco = entity.Endereco;
        model.Estado = entity.Estado;
        model.Inativacao = entity.Inativacao;
        model.Logradouro = entity.Logradouro;
        model.Nome = entity.Nome;
        model.Numero = entity.Numero;
        model.Sobrenome = entity.Sobrenome;
        model.Telefone = entity.Telefone;
        model.IdUsuario = entity.IdUsuario;

        model.Latitude = entity.Latitude;
        model.Longitude = entity.Longitude;
        model.PossuiCoordenadasGps = entity.PossuiCoordenadasGps;
    }

    internal static void MapColaborador(ColaboradorEntity entity, ColaboradorModel? model)
    {
        if (model is null) 
        {
            model = new ColaboradorModel();
            model.Id = entity.Id;
        }

        model.Alteracao = entity.Alteracao;
        model.Ativo = entity.Ativo;
        model.Bairro = entity.Bairro;
        model.Celular = entity.Celular;
        model.Cep = entity.Cep;
        model.Cidade = entity.Cidade;
        model.Cpf = entity.Cpf;
        model.Criacao = entity.Criacao;
        model.DddCelular = entity.DddCelular;
        model.DddTelefone = entity.DddTelefone;
        model.Email = entity.Email;
        model.Endereco = entity.Endereco;
        model.Estado = entity.Estado;
        model.Inativacao = entity.Inativacao;
        model.Logradouro = entity.Logradouro;
        model.Nome = entity.Nome;
        model.Numero = entity.Numero;
        model.Sobrenome = entity.Sobrenome;
        model.Telefone = entity.Telefone;
        model.IdUsuario = entity.IdUsuario;

        model.Latitude = entity.Latitude;
        model.Longitude = entity.Longitude;
        model.PossuiCoordenadasGps = entity.PossuiCoordenadasGps;
    }
}