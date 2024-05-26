using SuporteSolidarioBusiness.Domain.Entities;
using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Infrastructure.Mappers;

namespace SuporteSolidarioBusiness.Application.UseCases;

public class BuscarSolicitacoesPorProximidadeUseCase
{
    private readonly ISolicitacaoRepository _solicitacaoRepositorio;
    private readonly long _idColaborador;
    private readonly double _distanciaEmKm;

    public BuscarSolicitacoesPorProximidadeUseCase(ISolicitacaoRepository repo, long idColaborador, double distanciaEmKm)
    {
        _solicitacaoRepositorio = repo;
        _idColaborador = idColaborador;
        _distanciaEmKm = distanciaEmKm;
    }

    public IEnumerable<SolicitacaoDistanciaDTO> Execute()
    {
        IEnumerable<SolicitacaoDistanciaDTO> lst = _solicitacaoRepositorio.BuscarPorColaboradorDTO(_idColaborador, _distanciaEmKm);
        
        return lst;
    }

    public IEnumerable<SolicitacaoEntity> Execute2()
    {
        IEnumerable<SolicitacaoModel> lst = _solicitacaoRepositorio.BuscarPorColaborador(_idColaborador, _distanciaEmKm);

        IEnumerable<SolicitacaoEntity> lstSaida = ModelToEntity.MapListSolicitacao(lst);

        return lstSaida;
    }
}