using SuporteSolidarioBusiness.Domain.Entities;
using SuporteSolidarioBusiness.Infrastructure.MySQL;

namespace SuporteSolidarioBusiness.Infrastructure.Mappers;

public static class ModelToEntity
{
    internal static ClienteEntity MapCliente(ClienteModel model)
    {
        ClienteEntity entity = new ClienteEntity
        {
            Id = model.Id,
            IdUsuario = model.Id,
            Alteracao = model.Alteracao,
            Ativo = model.Ativo,
            Bairro = model.Bairro,
            Celular = model.Celular,
            Cep = model.Cep,
            Cidade = model.Cidade,
            Cpf = model.Cpf,
            Criacao = model.Criacao,
            DddCelular = model.DddCelular,
            DddTelefone = model.DddTelefone,
            Email = model.Email,
            Endereco = model.Endereco,
            Estado = model.Estado,
            
            Inativacao = model.Inativacao,
            Logradouro = model.Logradouro,
            Nome = model.Nome,
            Numero = model.Numero,
            Sobrenome = model.Sobrenome,
            Telefone = model.Telefone,

            Latitude = model.Latitude,
            Longitude = model.Longitude,
            PossuiCoordenadasGps = model.PossuiCoordenadasGps
        };
        
        return entity;
    }

    internal static ColaboradorEntity MapColaborador(ColaboradorModel model)
    {
        ColaboradorEntity entity = new ColaboradorEntity
        {
            Id = model.Id,
            IdUsuario = model.Id,
            Alteracao = model.Alteracao,
            Ativo = model.Ativo,
            Bairro = model.Bairro,
            Celular = model.Celular,
            Cep = model.Cep,
            Cidade = model.Cidade,
            Cpf = model.Cpf,
            Criacao = model.Criacao,
            DddCelular = model.DddCelular,
            DddTelefone = model.DddTelefone,
            Email = model.Email,
            Endereco = model.Endereco,
            Estado = model.Estado,
            
            Inativacao = model.Inativacao,
            Logradouro = model.Logradouro,
            Nome = model.Nome,
            Numero = model.Numero,
            Sobrenome = model.Sobrenome,
            Telefone = model.Telefone,

            Latitude = model.Latitude,
            Longitude = model.Longitude,
            PossuiCoordenadasGps = model.PossuiCoordenadasGps
        };
        
        return entity;
    }

    internal static ServicoEntity MapServico(ServicoModel model)
    {
        ServicoEntity entity = new ServicoEntity
        {
            Id = model.Id,
            Descricao = model.Descricao
        };

        return entity;
    }

    internal static CategoriaEntity MapCategoria(CategoriaModel model)
    {
        CategoriaEntity entity = new CategoriaEntity
        {
            Id = model.Id,
            Descricao = model.Descricao
        };

        return entity;
    }

    internal static IEnumerable<ServicoEntity> MapListServico(IEnumerable<ServicoModel> lst)
    {
        IEnumerable<ServicoEntity> lstEntity = new List<ServicoEntity>();
        foreach(ServicoModel item in lst)
        {
            lstEntity.Append(MapServico(item));
        }

        return lstEntity;
    }

    internal static IEnumerable<CategoriaEntity> MapListTipoServico(IEnumerable<CategoriaModel> lstModel)
    {
        IEnumerable<CategoriaEntity> lstEntity = new List<CategoriaEntity>();
        foreach(CategoriaModel item in lstModel)
        {
            lstEntity.Append(MapCategoria(item));
        }

        return lstEntity;
    }

    internal static ColaboradorServicoEntity MapColaboradorServico(ColaboradorServicoModel model)
    {
        ColaboradorServicoEntity entity = new ColaboradorServicoEntity
        {
            Id = model.Id,

            IdColaborador = model.Colaborador.Id,

            IdServico = model.Servico.Id
        };

        return entity;
    }

    internal static AtendimentoMensagemEntity MapAtendimentoMensagem(AtendimentoMensagemModel model)
    {
        AtendimentoMensagemEntity entity = new AtendimentoMensagemEntity
        {
            Id = model.Id,
            IdAtendimento = model.Atendimento.Id,
            Mensagem = model.Mensagem,
            IsCliente = model.IsCliente,
            IdEmResposta = model.IdEmResposta,
            Criacao = model.Criacao,
            Alteracao = model.Alteracao,
        };

        return entity;
    }

    internal static AtendimentoEntity MapAtendimento(AtendimentoModel model)
    {
        AtendimentoEntity entity = new AtendimentoEntity
        {
            Id = model.Id,
            AtendidoEm = model.AtendidoEm,
            Avaliacao = model.Avaliacao,
            IdColaborador = model.Colaborador.Id,
            ConfirmadoEm = model.ConfirmadoEm,
            FinalizadoEm = model.FinalizadoEm,
            IdSolicitacao = model.Solicitacao.Id
        };

        return entity;
    }

    internal static UsuarioEntity MapUsuario(UsuarioModel model)
    {
        UsuarioEntity entity = new UsuarioEntity
        {
            Id = model.Id,
            Celular = model.Celular,
            Email = model.Email,
            Login = model.Login,
            TipoDeUsuario = model.TipoDeUsuario,
            Ativo = model.Ativo
        };

        return entity;
    }

    internal static IEnumerable<ColaboradorEntity> MapListColaborador(IEnumerable<ColaboradorModel> lstModel)
    {
        IEnumerable<ColaboradorEntity> lstEntity = new List<ColaboradorEntity>();

        foreach(ColaboradorModel item in lstModel)
        {
            lstEntity.Append(MapColaborador(item));
        }

        return lstEntity;
    }

    internal static SolicitacaoEntity MapSolicitacao(SolicitacaoModel model)
    {
        SolicitacaoEntity entity = new SolicitacaoEntity
        {
            Id = model.Id,
            IdCliente = model.IdCliente,
            IdServico = model.IdServico,
            Data = model.Data,
            Detalhes = model.Detalhes,
            DataServico = model.DataServico,
            EmAberto = model.EmAberto,

            Bairro = model.Bairro,
            Cep = model.Cep,
            Cidade = model.Cidade,
            Endereco = model.Endereco,
            Estado = model.Estado,
            Logradouro = model.Logradouro,
            Numero = model.Numero,
            Latitude = model.Latitude,
            Longitude = model.Longitude
        };

        return entity;
    }

    internal static IEnumerable<SolicitacaoEntity> MapListSolicitacao(IEnumerable<SolicitacaoModel> lst)
    {
        List<SolicitacaoEntity> lstSaida = new List<SolicitacaoEntity>();

        foreach(SolicitacaoModel item in lst)
        {
            lstSaida.Add(MapSolicitacao(item));
        }

        return lstSaida;
    }
}