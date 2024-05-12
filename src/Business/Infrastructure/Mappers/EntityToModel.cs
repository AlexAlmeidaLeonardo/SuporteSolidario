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
            Atendimento = new AtendimentoModel
            {
                Id = entity.IdAtendimento
            },

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
            Colaborador = new ColaboradorModel
            {
                Id = entity.Id
            },
            ConfirmadoEm = entity.ConfirmadoEm,
            FinalizadoEm = entity.FinalizadoEm,
            Solicitacao = new SolicitacaoModel
            {
                Id = entity.Id
            }
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
}