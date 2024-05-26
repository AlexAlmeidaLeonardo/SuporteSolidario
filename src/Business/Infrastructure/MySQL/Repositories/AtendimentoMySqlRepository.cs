using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Domain.Entities;
using SuporteSolidarioBusiness.Infrastructure.Mappers;

namespace SuporteSolidarioBusiness.Infrastructure.MySQL.Repositories;

public class AtendimentoMySqlRepository: IAtendimentoRepository
{
    private readonly SuporteSolidarioDbContext _context;

    public AtendimentoMySqlRepository(SuporteSolidarioDbContext context)
    {
        _context = context;
    }

    public AtendimentoEntity Adicionar(AtendimentoEntity obj)
    {
        try
        {
            ColaboradorModel colaborador = _context.Colaboradores.Where(x => x.Id == obj.IdColaborador).FirstOrDefault();
            if(colaborador == null)
            {
                throw new Exception("Colaborador não encontrado");
            }

            AtendimentoModel model = EntityToModel.MapAtendimento(obj);
            model.AtendidoEm = DateTime.Now;
            model.Colaborador = colaborador;

            _context.Atendimentos.Add(model);

            _context.SaveChanges();

            AtendimentoEntity entity = ModelToEntity.MapAtendimento(model);

            return entity;
        }
        catch(Exception e)
        {
            throw e;
        }
    }

    public bool SetAsAtendido(long id)
    {
        AtendimentoModel? model = _context.Atendimentos.Where(x => x.Id == id).FirstOrDefault();

        if(model is null)
        {
            throw new NotFoundException($"Atendimento com id {id} não encontrado");
        }

        model.AtendidoEm = DateTime.Now;

        _context.Atendimentos.Update(model);

        int affecteds =_context.SaveChanges();

        return affecteds > 0;

    }

    public bool SetAsConfirmado(long id)
    {
        AtendimentoModel? model = _context.Atendimentos.Where(x => x.Id == id).FirstOrDefault();

        if(model is null)
        {
            throw new NotFoundException($"Atendimento com id {id} não encontrado");
        }

        model.ConfirmadoEm = DateTime.Now;

        _context.Atendimentos.Update(model);

        int affecteds =_context.SaveChanges();

        return affecteds > 0;
    }

    public bool SetAsFinalizado(long id)
    {
        AtendimentoModel? model = _context.Atendimentos.Where(x => x.Id == id).FirstOrDefault();

        if(model is null)
        {
            throw new NotFoundException($"Atendimento com id {id} não encontrado");
        }

        model.FinalizadoEm = DateTime.Now;

        _context.Atendimentos.Update(model);

        int affecteds =_context.SaveChanges();

        return affecteds > 0;
    }

    public bool SetAsRecusado(long id)
    {
        AtendimentoModel? model = _context.Atendimentos.Where(x => x.Id == id).FirstOrDefault();

        if(model is null)
        {
            throw new NotFoundException($"Atendimento com id {id} não encontrado");
        }

        model.RecusadoEm = DateTime.Now;

        _context.Atendimentos.Update(model);

        int affecteds =_context.SaveChanges();

        return affecteds > 0;
    }

    public bool SetAvaliacao(long id, int avaliacao)
    {
        AtendimentoModel? model = _context.Atendimentos.Where(x => x.Id == id).FirstOrDefault();

        if(model is null)
        {
            throw new NotFoundException($"Atendimento com id {id} não encontrado");
        }

        model.Avaliacao = avaliacao;

        _context.Atendimentos.Update(model);

        int affecteds =_context.SaveChanges();

        return affecteds > 0;
    }
}