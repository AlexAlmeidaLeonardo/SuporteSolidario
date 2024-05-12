namespace SuporteSolidarioBusiness.Application.Repositories;

using SuporteSolidarioBusiness.Domain.Entities;

public interface ICategoriaRepository
{
    CategoriaEntity Ler(long id);
    CategoriaEntity Adicionar(CategoriaEntity obj);
    IEnumerable<CategoriaEntity> LerTodos();
}