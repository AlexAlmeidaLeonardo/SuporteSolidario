namespace SuporteSolidarioBusiness.Application.Repositories;

using SuporteSolidarioBusiness.Application.DTOs;
using SuporteSolidarioBusiness.Domain.Entities;

public interface IAtendimentoMensagemRepository
{
    AtendimentoMensagemEntity MensagemCliente (AtendimentoMensagemEntity obj);

    AtendimentoMensagemEntity MensagemColaborador (AtendimentoMensagemEntity obj);

    List<AtendimentoMensagemDTO> GetMensagens(long idAtendimento);
}