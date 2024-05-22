namespace SuporteSolidarioBusiness.Infrastructure.MySQL.Repositories;

using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Domain.Entities;
using SuporteSolidarioBusiness.Infrastructure.Mappers;
using SuporteSolidarioBusiness.Infrastructure.MySQL;


public class SolicitacaoRepository : ISolicitacaoRepository
{
    private readonly SuporteSolidarioDbContext _context;

    public SolicitacaoRepository(SuporteSolidarioDbContext context)
    {
        this._context = context;
    }

    public SolicitacaoEntity Adicionar(SolicitacaoEntity entity)
    {
        SolicitacaoModel model = EntityToModel.MapSolicitacao(entity);

        _context.Solicitacoes.Add(model);

        _context.SaveChanges();

        SolicitacaoEntity entityOut = ModelToEntity.MapSolicitacao(model);

        return entityOut;
    }

    public IEnumerable<SolicitacaoModel> BuscarPorColaborador(long idColaborador, double _distanciaEmKm)
    {
        ColaboradorModel colaborador = _context.Colaboradores.Where(x => x.Id == idColaborador).FirstOrDefault();
        if(colaborador == null)
        {
            throw new NotFoundException($"Colaborador com id {idColaborador} não encontrado.");
        }

        IEnumerable<long> lstIdServicos = _context.ColaboradorServicos
                                            .Where(x => x.Id == idColaborador)
                                            .Select(x => x.IdServico)
                                            .ToList();

        IEnumerable<SolicitacaoModel> lstSaida = _context.Solicitacoes
                                                    .Where(solicitacao => solicitacao.EmAberto
                                                                       && lstIdServicos.Contains(solicitacao.IdServico) 
                                                                       && Math.Sqrt(
                                                                                     Math.Pow(111.045 *(solicitacao.Latitude - colaborador.Latitude), 2) 
                                                                                     +
                                                                                     Math.Pow(111.045 *(colaborador.Longitude - solicitacao.Longitude) * Math.Cos(solicitacao.Latitude / 57.3), 2) ) <= _distanciaEmKm);

        return lstSaida;
    }

    public SolicitacaoModel GetSolicitacaoAberta(long idCliente, long idServico)
    {
        SolicitacaoModel saida = _context.Solicitacoes
                                    .Where(   x => x.IdCliente == idCliente
                                           && x.IdServico == idServico
                                           && x.EmAberto).FirstOrDefault();

        return saida;
    }

    public SolicitacaoEntity Ler(long id)
    {
        SolicitacaoModel? model = _context.Solicitacoes.Where(x => x.Id == id).FirstOrDefault();

        if(model is null)
        {
            throw new NotFoundException($"Solicitação com id {id} não encontrada");
        }

        SolicitacaoEntity entity = ModelToEntity.MapSolicitacao(model);

        return entity;
    }

    public IEnumerable<SolicitacaoModel> GetSolicitacoesAbertas(long idCliente)
    {
        IEnumerable<SolicitacaoModel> lst = _context.Solicitacoes
                                                    .Where( x => x.IdCliente == idCliente && x.EmAberto)
                                                    .OrderBy(o => o.Data)
                                                    .ToList();
        return lst;
    }

    public IEnumerable<SolicitacaoModel> GetHistoricoByCliente(long idCliente)
    {
        IEnumerable<SolicitacaoModel> lst = _context.Solicitacoes
                                                    .Where( x => x.IdCliente == idCliente && !x.EmAberto)
                                                    .OrderBy(o => o.Data)
                                                    .ToList();
        return lst;
    }

    public IEnumerable<SolicitacaoEmAbertoDTO> GetSolicitacoesEmAberto(long idCliente)
    {
        
        IEnumerable<SolicitacaoEmAbertoDTO> lst = _context.Solicitacoes
                                                    .Join(
                                                        _context.Servicos,
                                                        solicitacao => solicitacao.IdServico,
                                                        servico => servico.Id,
                                                        (solicitacao, servico) => new 
                                                        {
                                                            Id = solicitacao.Id,
                                                            IdCliente = solicitacao.IdCliente,
                                                            IdServico = servico.Id,
                                                            DescricaoCategoria = servico.Categoria.Descricao,                                                            
                                                            DescricaoServico = servico.Descricao,
                                                            Data = solicitacao.Data,
                                                            DataServico = solicitacao.DataServico,
                                                            Detalhes = solicitacao.Detalhes,
                                                            EmAberto = solicitacao.EmAberto
                                                        })
                                                    .Where( x => x.IdCliente == idCliente && x.EmAberto == true)
                                                    .OrderBy(o => o.Data)
                                                    .Select(x => new SolicitacaoEmAbertoDTO()
                                                    {
                                                        Id = x.Id,
                                                        IdCliente = x.IdCliente,
                                                        IdServico = x.IdServico,
                                                        DescricaoCategoria = x.DescricaoCategoria,
                                                        DescricaoServico = x.DescricaoServico,
                                                        Data = x.Data,
                                                        DataServico = x.DataServico,
                                                        Detalhes = x.Detalhes
                                                    })
                                                    .ToList();

        return lst;
    }
}