namespace SuporteSolidarioBusiness.Infrastructure.MySQL.Repositories;

using System.Collections.Generic;
using SuporteSolidarioBusiness.Domain.Entities;
using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Infrastructure.Mappers;
using SuporteSolidarioBusiness.Application.DTOs;

public class ColaboradorMySqlRepository : IColaboradorRepository
{
    private readonly SuporteSolidarioDbContext _context;
    public ColaboradorMySqlRepository(SuporteSolidarioDbContext context)
    {
        _context = context;
    }

    public ColaboradorEntity Ler(long id)
    {
        ColaboradorModel? model = _context.Colaboradores.Where(x => x.Id == id).FirstOrDefault();
        
        if(model is null)
        {
            throw new NotFoundException($"Colaborador com id {id} não encontrado");
        }

        ColaboradorEntity entity = ModelToEntity.MapColaborador(model);

        return entity;
    }

    public ColaboradorEntity Adicionar(ColaboradorEntity entity)
    {
        ColaboradorModel model = EntityToModel.MapColaborador(entity);
        model.Criacao = DateTime.Now;

        _context.Colaboradores.Add(model);

        _context.SaveChanges();

        ColaboradorEntity entityOut = ModelToEntity.MapColaborador(model);

        return entityOut;
    }

    public ColaboradorEntity Atualizar(ColaboradorEntity entity)
    {
        ColaboradorModel? entityModel = _context.Colaboradores.Find(entity.Id);
        if(entityModel is null)
        {
            throw new NotFoundException($"Colaborador com id {entity.Id} não encontrado");
        }
        
        EntityToModel.MapColaborador(entity, entityModel);
        entityModel.Alteracao = DateTime.Now;

        _context.Colaboradores.Update(entityModel);

        _context.SaveChanges();

        ColaboradorEntity entityOut = ModelToEntity.MapColaborador(entityModel);

        return entityOut;
    }

    public bool ExisteLogin(long id)
    {
        bool existe = _context.Colaboradores
                        .Where(x => x.IdUsuario == id)
                        .Any();

        return existe;
    }

    public IEnumerable<ColaboradorEntity> Todos()
    {
        IEnumerable<ColaboradorModel> models = _context.Colaboradores.Where(x => x.Inativacao == null).ToList();

        return ModelToEntity.MapListColaborador(models);        
    }

    public ColaboradorEntity LerByUsuario(long id)
    {
        ColaboradorModel? model = _context.Colaboradores.Where(x => x.IdUsuario == id).FirstOrDefault();
        
        if(model is null)
        {
            return null;
            //throw new NotFoundException($"Colaborador com id de usuário {id} não encontrado");
        }

        ColaboradorEntity entity = ModelToEntity.MapColaborador(model);

        return entity;
    }

    public DetalhesSolicitacaoDTO GetDetalhesSolicitacao(long id)
    {
        IQueryable<DetalhesSolicitacaoDTO> 
                queryDetalhes =   from ColaboradorSolicitacao in _context.ColaboradorServicos
                                  join Solicitacao            in _context.Solicitacoes        on ColaboradorSolicitacao.IdServico equals Solicitacao.IdServico
                                  join Cliente                in _context.Clientes            on Solicitacao.IdCliente            equals Cliente.Id
                                  join Servico                in _context.Servicos            on ColaboradorSolicitacao.IdServico equals Servico.Id
                                  join Categoria              in _context.Categorias          on Servico.IdCategoria              equals Categoria.Id
                                 where ColaboradorSolicitacao.IdColaborador == id
                                select new DetalhesSolicitacaoDTO
                                {
                                    Id = Solicitacao.Id,
                                    Nome = Cliente.Nome + " " + Cliente.Sobrenome,
                                    Categoria = Categoria.Descricao,
                                    Servico = Servico.Descricao,
                                    Distancia = 0.0,
                                    DataServico = Solicitacao.DataServico,
                                    Detalhes = Solicitacao.Detalhes
                                };

        DetalhesSolicitacaoDTO retorno = queryDetalhes.FirstOrDefault();
        return retorno;
    }

    public ColaboradorDTO BuscarPorAtendimento(long idAtendimento)
    {
        IQueryable<ColaboradorDTO> queryColaborador =    from A in _context.Atendimentos
                                                         join C in _context.Colaboradores on A.IdColaborador equals C.Id
                                                       select new ColaboradorDTO
                                                       {
                                                            Nome = C.Nome,
                                                            Sobrenome = C.Sobrenome
                                                       };
                                                                
        ColaboradorDTO retorno = queryColaborador.FirstOrDefault();
        return retorno;
    }
}



/*
  select s.id,
         CONCAT(c.nome, ' ', c.sobrenome) as Nome,
         cat.descricao as Categoria,
         servico.descricao as Servico,
         0.0 as Distancia,
         s.data_servico,
         s.detalhes         
         
    from colaborador_servicos cs join solicitacoes s
                                   on cs.id_servico = s.id_servico
                                 join clientes c 
                                   on s.id_cliente = c.id
                                 join servicos servico
                                   on cs.id_servico = servico.id
                                 join categorias cat
                                   on servico.id_categoria = cat.id
   where cs.id_colaborador = 1
*/