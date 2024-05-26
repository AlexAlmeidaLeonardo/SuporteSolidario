using SuporteSolidarioBusiness.Domain.Entities;

namespace SuporteSolidarioBusiness.Application.Repositories;

public interface IAtendimentoRepository
{
    AtendimentoEntity Adicionar(AtendimentoEntity entity);

    bool SetAsAtendido(long id);

    bool SetAsConfirmado(long id);

    bool SetAsFinalizado(long id);

    bool SetAsRecusado(long id);

    bool SetAvaliacao(long id, int avaliacao);
}