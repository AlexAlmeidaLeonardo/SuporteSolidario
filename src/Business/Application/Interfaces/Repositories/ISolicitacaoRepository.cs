using SuporteSolidarioBusiness.Application.DTOs;
using SuporteSolidarioBusiness.Domain.Entities;

namespace SuporteSolidarioBusiness.Application.Repositories;
public interface ISolicitacaoRepository
{
    SolicitacaoEntity Ler(long id);
    SolicitacaoEntity Adicionar(SolicitacaoEntity obj);
    IEnumerable<SolicitacaoModel> BuscarPorColaborador(long idColaborador, double _distanciaEmKm);
    IEnumerable<SolicitacaoDistanciaDTO> BuscarPorColaboradorDTO(long idColaborador, double _distanciaEmKm);
    SolicitacaoModel GetSolicitacaoAberta(long idCliente, long idServico);
    IEnumerable<SolicitacaoModel> GetSolicitacoesAbertas(long idCliente);
    IEnumerable<SolicitacaoModel> GetHistoricoByCliente(long idCliente);
    
    IEnumerable<SolicitacaoEmAbertoDTO> GetSolicitacoesEmAberto(long idCliente);
}
