namespace SuporteSolidarioBusiness.Application.Repositories;

using SuporteSolidarioBusiness.Domain.Entities;

public interface IAtendimentoMensagemRepository
{
    AtendimentoMensagemEntity MensagemCliente (AtendimentoMensagemEntity obj);

    AtendimentoMensagemEntity MensagemColaborador (AtendimentoMensagemEntity obj);
}