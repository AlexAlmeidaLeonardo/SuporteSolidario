namespace SuporteSolidarioBusiness.Infrastructure.MySQL.Repositories;

using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Domain.Entities;
using SuporteSolidarioBusiness.Infrastructure.Mappers;

public class ColaboradorServicoMySqlRepository : IColaboradorServicoRepository
{
    private readonly SuporteSolidarioDbContext _context;
    public ColaboradorServicoMySqlRepository(SuporteSolidarioDbContext context)
    {
        _context = context;
    }

    public ColaboradorServicoEntity Ler(long id)
    {
        ColaboradorServicoModel? model = _context.ColaboradorServicos.Where(x => x.Id == id).FirstOrDefault();

        if(model is null)
        {
            throw new NotFoundException($"Serviço do colaborador com id {id} não encontrado");
        }

        ColaboradorServicoEntity entity = ModelToEntity.MapColaboradorServico(model);

        return entity;
    }

    public ColaboradorServicoEntity Adicionar(ColaboradorServicoEntity obj)
    {
        ColaboradorServicoModel model = EntityToModel.MapColaboradorServico(obj);
        model.Colaborador = _context.Colaboradores.Where(x => x.Id == obj.IdColaborador).FirstOrDefault();
        model.Servico = _context.Servicos.Where(x => x.Id == obj.IdServico).FirstOrDefault();

        _context.ColaboradorServicos.Add(model);

        _context.SaveChanges();

        ColaboradorServicoEntity entity = ModelToEntity.MapColaboradorServico(model);

        return entity;
    }
}