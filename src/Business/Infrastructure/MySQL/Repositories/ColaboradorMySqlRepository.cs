namespace SuporteSolidarioBusiness.Infrastructure.MySQL.Repositories;

using System.Collections.Generic;
using SuporteSolidarioBusiness.Domain.Entities;
using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Infrastructure.Mappers;

public class ColaboradorMySqlRepository : IColaboradorRepository
{
    private readonly SuporteSolidarioDbContext _context;
    public ColaboradorMySqlRepository(SuporteSolidarioDbContext context)
    {
        _context = context;
    }

    public ColaboradorEntity Ler(long id)
    {
        ColaboradorModel? model = _context.Colaboradores.Where(x => x.Id == id).FirstOrDefault();

        if(model is null)
        {
            return null;
        }

        ColaboradorEntity entity = ModelToEntity.MapColaborador(model);

        return entity;
    }

    public ColaboradorEntity Adicionar(ColaboradorEntity obj)
    {
        ColaboradorModel model = EntityToModel.MapColaborador(obj);
        model.Criacao = DateTime.Now;

        _context.Colaboradores.Add(model);

        _context.SaveChanges();

        ColaboradorEntity entity = ModelToEntity.MapColaborador(model);

        return entity;
    }

    public ColaboradorEntity Atualizar(ColaboradorEntity obj)
    {
        ColaboradorModel? entityModel = _context.Colaboradores.Find(obj.Id);
        if(entityModel is null)
        {
            throw new NotFoundException($"Colaborador com id {obj.Id} não encontrado");
        }
        
        ColaboradorModel newModel = EntityToModel.MapColaborador(obj);

        entityModel = ModelToModel.MapColaborador(entityModel, newModel);
        entityModel.Alteracao = DateTime.Now;

        _context.Colaboradores.Update(entityModel);

        _context.SaveChanges();

        ColaboradorEntity entity = ModelToEntity.MapColaborador(entityModel);

        return entity;
    }

    public bool ExisteLogin(long id)
    {
        bool existe = _context.Colaboradores
                        .Where(x => x.IdUsuario == id)
                        .Any();

        return existe;
    }

    public IEnumerable<ColaboradorEntity> Todos()
    {
        IEnumerable<ColaboradorModel> models = _context.Colaboradores.Where(x => x.Inativacao == null).ToList();

        return ModelToEntity.MapListColaborador(models);        
    }
}