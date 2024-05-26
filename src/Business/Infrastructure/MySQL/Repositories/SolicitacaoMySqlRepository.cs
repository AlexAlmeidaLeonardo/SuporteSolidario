namespace SuporteSolidarioBusiness.Infrastructure.MySQL.Repositories;

using SuporteSolidarioBusiness.Application.DTOs;
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
        IEnumerable<SolicitacaoEmAbertoDTO> lst =   from S in _context.Solicitacoes
                                                   where S.IdCliente == idCliente 
                                                      && S.EmAberto == true
                                                      && !_context.Atendimentos.Select(a => a.IdSolicitacao).Contains(S.Id) 
                                                 orderby S.Data
                                                  select new SolicitacaoEmAbertoDTO()
                                                  {
                                                        Id = S.Id,
                                                        IdCliente = S.IdCliente,
                                                        IdServico = S.IdServico,
                                                        DescricaoCategoria = S.Servico.Categoria.Descricao,
                                                        DescricaoServico = S.Servico.Descricao,
                                                        Data = S.Data,
                                                        DataServico = S.DataServico,
                                                        Detalhes = S.Detalhes
                                                 };
        return lst;
    }

    double CalcularDistancia(double lat1, double lon1, double lat2, double lon2)
    {
        var dLat = (lat2 - lat1) * (Math.PI / 180.0);
        var dLon = (lon2 - lon1) * (Math.PI / 180.0);

        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(lat1 * (Math.PI / 180.0)) * Math.Cos(lat2 * (Math.PI / 180.0)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        var d = 6371 * c;
        return d;
    }
    
    public IEnumerable<SolicitacaoDistanciaDTO> BuscarPorColaboradorDTO(long idColaborador, double distanciaEmKm)
    {
        List<SolicitacaoDistanciaDTO> lstRetorno = new List<SolicitacaoDistanciaDTO>();

        IQueryable<ColaboradorModel> queryColaborador =   from c in _context.Colaboradores
                                                         where c.Id == idColaborador
                                                        select c;

        ColaboradorModel colaborador = queryColaborador.FirstOrDefault();

        if(colaborador == null)
            return lstRetorno;
        
        var result = from s in _context.Solicitacoes
             join y in _context.ColaboradorServicos on s.IdServico equals y.IdServico
             join x in _context.Servicos on s.IdServico equals x.Id
             select new SolicitacaoDistanciaDTO()
             {
                 Id = s.Id,
                 DescricaoServico = x.Descricao,
                 Latitude = s.Latitude,
                 Longitude = s.Longitude,
                 DataDaSolicitacao = s.Data,
                 DataDoServico = s.DataServico,
                 DistanciaEmKm = -1
             };

        lstRetorno = result.ToList();

        foreach(SolicitacaoDistanciaDTO item in lstRetorno)
        {
            item.DistanciaEmKm = CalcularDistancia(colaborador.Latitude, colaborador.Longitude, item.Latitude, item.Longitude);
        }

        return lstRetorno;
    }

    public IEnumerable<SolicitacaoEmAbertoDTO> GetSolicitacoesAtendidas(long idCliente)
    {
         IEnumerable<SolicitacaoEmAbertoDTO> lst =   from S in _context.Solicitacoes
                                                   where S.IdCliente == idCliente
                                                       && S.EmAberto == false
                                                 orderby S.Data
                                                  select new SolicitacaoEmAbertoDTO()
                                                  {
                                                        Id = S.Id,
                                                        IdCliente = S.IdCliente,
                                                        IdServico = S.IdServico,
                                                        DescricaoCategoria = S.Servico.Categoria.Descricao,
                                                        DescricaoServico = S.Servico.Descricao,
                                                        Data = S.Data,
                                                        DataServico = S.DataServico,
                                                        Detalhes = S.Detalhes
                                                 };
        return lst;
    }

    public List<SolicitacaoEmAndamentoDTO> BuscarSolicitacoesComAtendimentos(long idCliente)
    {
        IQueryable<SolicitacaoEmAndamentoDTO> queryRetorno =      from S            in _context.Solicitacoes
                                                                  join A            in _context.Atendimentos on S.Id equals A.IdSolicitacao
                                                                  join Servico      in _context.Servicos on S.IdServico equals Servico.Id
                                                                  join Categoria    in _context.Categorias on Servico.IdCategoria equals Categoria.Id
                                                                  join Colaborador  in _context.Colaboradores on A.IdColaborador equals Colaborador.Id
                                                                select new SolicitacaoEmAndamentoDTO
                                                                {
                                                                    Id = S.Id,
                                                                    IdCliente = S.IdCliente,
                                                                    IdServico = S.IdServico,
                                                                    IdAtendimento = A.Id,
                                                                    IdSolicitacao = S.Id,
                                                                    DescricaoCategoria = Categoria.Descricao,
                                                                    DescricaoServico = Servico.Descricao,
                                                                    Data = S.Data,
                                                                    DataServico = S.DataServico,
                                                                    Detalhes = S.Detalhes,
                                                                    Colaborador = Colaborador.Nome + " " + Colaborador.Sobrenome
                                                                };
                                                                        
        return queryRetorno.ToList();
    }
}