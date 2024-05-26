using SuporteSolidarioBusiness.Application.DTOs;
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
        model.Criacao = DateTime.Now;
        model.Alteracao = model.Criacao;

        _context.AtendimentoMensagens.Add(model);

        _context.SaveChanges();

        AtendimentoMensagemEntity entity = ModelToEntity.MapAtendimentoMensagem(model);

        return entity;
    }

    public AtendimentoMensagemEntity MensagemColaborador(AtendimentoMensagemEntity obj)
    {
        AtendimentoMensagemModel model = EntityToModel.MapAtendimentoMensagem(obj);
        model.IsCliente = false;
        model.Criacao = DateTime.Now;
        model.Alteracao = model.Criacao;

        _context.AtendimentoMensagens.Add(model);

        _context.SaveChanges();

        AtendimentoMensagemEntity entity = ModelToEntity.MapAtendimentoMensagem(model);

        return entity;
    }

    public List<AtendimentoMensagemDTO> GetMensagens(long idAtendimento)
    {
        IQueryable<AtendimentoMensagemDTO> lst =   from AM in _context.AtendimentoMensagens
                                                  where AM.IdAtendimento == idAtendimento
                                                orderby AM.Id
                                                select new AtendimentoMensagemDTO
                                                {
                                                    IdAtendimento = AM.IdAtendimento,
                                                    Mensagem = AM.Mensagem,
                                                    IsCliente = AM.IsCliente,
                                                    Criacao = AM.Criacao
                                                };

        List<AtendimentoMensagemDTO> lstRetorno = lst.ToList();
        return lstRetorno;
    }

}