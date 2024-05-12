using SuporteSolidarioBusiness.Domain.Entities;
using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Infrastructure.Mappers;

namespace SuporteSolidarioBusiness.Application.UseCases;

public class BuscarSolicitacoesPorProximidadeUseCase
{
    private readonly ISolicitacaoRepository _repo;
    private readonly long _idColaborador;
    private readonly double _distanciaEmKm;

    public BuscarSolicitacoesPorProximidadeUseCase(ISolicitacaoRepository repo, long idColaborador, double distanciaEmKm)
    {
        _repo = repo;
        _idColaborador = idColaborador;
        _distanciaEmKm = distanciaEmKm;
    }

    public IEnumerable<SolicitacaoEntity> Execute()
    {
        IEnumerable<SolicitacaoModel> lst = _repo.BuscarPorColaborador(_idColaborador, _distanciaEmKm);

        IEnumerable<SolicitacaoEntity> lstSaida = ModelToEntity.MapListSolicitacao(lst);

        return lstSaida;
    }
}