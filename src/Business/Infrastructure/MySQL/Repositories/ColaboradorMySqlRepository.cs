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
            throw new NotFoundException($"Colaborador com id {id} não encontrado");
        }

        ColaboradorEntity entity = ModelToEntity.MapColaborador(model);

        return entity;
    }

    public ColaboradorEntity Adicionar(ColaboradorEntity entity)
    {
        ColaboradorModel model = EntityToModel.MapColaborador(entity);
        model.Criacao = DateTime.Now;

        _context.Colaboradores.Add(model);

        _context.SaveChanges();

        ColaboradorEntity entityOut = ModelToEntity.MapColaborador(model);

        return entityOut;
    }

    public ColaboradorEntity Atualizar(ColaboradorEntity entity)
    {
        ColaboradorModel? entityModel = _context.Colaboradores.Find(entity.Id);
        if(entityModel is null)
        {
            throw new NotFoundException($"Colaborador com id {entity.Id} não encontrado");
        }
        
        EntityToModel.MapColaborador(entity, entityModel);
        entityModel.Alteracao = DateTime.Now;

        _context.Colaboradores.Update(entityModel);

        _context.SaveChanges();

        ColaboradorEntity entityOut = ModelToEntity.MapColaborador(entityModel);

        return entityOut;
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

    public ColaboradorEntity LerByUsuario(long id)
    {
        ColaboradorModel? model = _context.Colaboradores.Where(x => x.IdUsuario == id).FirstOrDefault();
        
        if(model is null)
        {
            throw new NotFoundException($"Colaborador com id de usuário {id} não encontrado");
        }

        ColaboradorEntity entity = ModelToEntity.MapColaborador(model);

        return entity;
    }
}