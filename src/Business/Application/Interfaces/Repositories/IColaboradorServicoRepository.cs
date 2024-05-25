using SuporteSolidarioBusiness.Application.DTOs;
using SuporteSolidarioBusiness.Domain.Entities;

namespace SuporteSolidarioBusiness.Application.Repositories;

public interface IColaboradorServicoRepository
{
    ColaboradorServicoEntity Ler(long id);
    ColaboradorServicoEntity Adicionar(ColaboradorServicoEntity obj);

    IEnumerable<ColaboradorServicoEntity> LerTodos(long idColaborador);

    IEnumerable<ColaboradorServicoDTO> GetServicosByColaborador(long idColaborador);

    IEnumerable<ServicoDTO> GetServicosNaoPrestadosPorColaborador(long idColaborador);

    IEnumerable<ServicoDTO> GetServicosNaoPrestadosPorColaborador(long idColaborador, string descricao);

    void Remover(long id);
}