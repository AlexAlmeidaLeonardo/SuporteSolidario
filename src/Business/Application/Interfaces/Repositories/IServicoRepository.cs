namespace SuporteSolidarioBusiness.Application.Repositories;

using SuporteSolidarioBusiness.Domain.Entities;

public interface IServicoRepository
{
    ServicoEntity Ler(long id);
    ServicoEntity Adicionar(ServicoEntity obj);
    IEnumerable<ServicoEntity> LerTodos();
    IEnumerable<ServicoEntity> LerTodosPorCategoria(long idCategoria);
}