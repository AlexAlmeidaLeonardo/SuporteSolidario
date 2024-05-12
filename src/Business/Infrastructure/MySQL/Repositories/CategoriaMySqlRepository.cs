using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Domain.Entities;
using SuporteSolidarioBusiness.Infrastructure.Mappers;
using SuporteSolidarioBusiness.Infrastructure.MySQL;
using SuporteSolidarioBusiness.Infrastructure.MySQL.Repositories;

public class CategoriaMySqlRepository : ICategoriaRepository
{
    private readonly SuporteSolidarioDbContext _context;

    public CategoriaMySqlRepository(SuporteSolidarioDbContext context)
    {
        _context = context;
    }

    public IEnumerable<CategoriaEntity> LerTodos()
    {
        IEnumerable<CategoriaModel> lstModel = _context.Categorias.ToList();
        IEnumerable<CategoriaEntity> lstDto = ModelToEntity.MapListTipoServico(lstModel);
        return lstDto;
    }

    public CategoriaEntity Ler(long id)
    {
        CategoriaModel? model = _context.Categorias.Where(x => x.Id == id).FirstOrDefault();
        if(model is null)
        {
            throw new NotFoundException($"Categoria com id {id} não encontrado");
        }

        CategoriaEntity entity = ModelToEntity.MapCategoria(model);

        return entity;
    }

    public CategoriaEntity Adicionar(CategoriaEntity entity)
    {
        CategoriaModel model = EntityToModel.MapCategoria(entity);

        _context.Categorias.Add(model);

        _context.SaveChanges();

        CategoriaEntity entityOut = ModelToEntity.MapCategoria(model);

        return entityOut;
    }
}