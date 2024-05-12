namespace SuporteSolidarioBusiness.Domain.Entities;

public class AtendimentoMensagemEntity: BaseEntity
{
    public long IdAtendimento { get; set; }

    public DateTime Criacao { get; set; }
    
    public DateTime Alteracao { get; set; }

    public bool IsCliente { get; set; }

    public string Mensagem { get; set; }

    public long IdEmResposta { get; set; }
}