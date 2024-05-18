namespace SuporteSolidarioBusiness.Infrastructure.MySQL.Repositories;

using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Domain.Entities;
using SuporteSolidarioBusiness.Infrastructure.Mappers;

public class ServicoMySqlRepository : IServicoRepository
{
    private readonly SuporteSolidarioDbContext _context;

    public ServicoMySqlRepository(SuporteSolidarioDbContext context)
    {
        _context = context;
    }

    public IEnumerable<ServicoEntity> LerTodos()
    {
        IEnumerable<ServicoModel> lstModel = _context.Servicos.ToList();

        if(lstModel is null)
        {
            return null;
        }

        IEnumerable<ServicoEntity> lstDto = ModelToEntity.MapListServico(lstModel);

        return lstDto;
    }

    public ServicoEntity Ler(long id)
    {
        ServicoModel? model = _context.Servicos.Where(x => x.Id == id).FirstOrDefault();

        if(model is null)
        {
            throw new NotFoundException($"Servico com id {id} não encontrado");
        }

        ServicoEntity entity = ModelToEntity.MapServico(model);

        return entity;
    }

    public ServicoEntity Adicionar(ServicoEntity obj)
    {
        CategoriaModel categoria = _context.Categorias.Where(x => x.Id == obj.IdCategoria).FirstOrDefault();
        if(categoria == null)
        {
            throw new NotFoundException($"Categoria com id {obj.IdCategoria} não encontrada!");
        }

        ServicoModel model = EntityToModel.MapServico(obj);
        model.Categoria = categoria;
        
        _context.Servicos.Add(model);

        _context.SaveChanges();

        ServicoEntity entity = ModelToEntity.MapServico(model);

        return entity;
    }

    public IEnumerable<ServicoEntity> LerTodosPorCategoria(long idCategoria)
    {
        IEnumerable<ServicoModel> lstModel = _context.Servicos
                                                     .Where(x => x.IdCategoria == idCategoria)
                                                     .ToList();

        if(lstModel is null)
        {
            return null;
        }

        IEnumerable<ServicoEntity> lstDto = ModelToEntity.MapListServico(lstModel);

        return lstDto;
    }
}