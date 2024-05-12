using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Domain.Entities;
using SuporteSolidarioBusiness.Infrastructure.Mappers;

namespace SuporteSolidarioBusiness.Infrastructure.MySQL.Repositories;
public class AtendimentoMensagemMySqlRepository : IAtendimentoMensagemRepository
{
    private readonly SuporteSolidarioDbContext _context;

    public AtendimentoMensagemMySqlRepository(SuporteSolidarioDbContext context)
    {
        _context = context;
    }

    public AtendimentoMensagemEntity MensagemCliente(AtendimentoMensagemEntity obj)
    {
        AtendimentoMensagemModel model = EntityToModel.MapAtendimentoMensagem(obj);
        model.IsCliente = true;

        _context.AtendimentoMensagens.Add(model);

        _context.SaveChanges();

        AtendimentoMensagemEntity entity = ModelToEntity.MapAtendimentoMensagem(model);

        return entity;
    }

    public AtendimentoMensagemEntity MensagemColaborador(AtendimentoMensagemEntity obj)
    {
        AtendimentoMensagemModel model = EntityToModel.MapAtendimentoMensagem(obj);
        model.IsCliente = false;

        _context.AtendimentoMensagens.Add(model);

        _context.SaveChanges();

        AtendimentoMensagemEntity entity = ModelToEntity.MapAtendimentoMensagem(model);

        return entity;
    }
}