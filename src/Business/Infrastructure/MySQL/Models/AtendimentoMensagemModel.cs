using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SuporteSolidarioBusiness.Infrastructure.MySQL;

public class AtendimentoMensagemModel: BaseModel
{
    [Column(Order = 1)]
    [ForeignKey("Atendimento")]    
    public long IdAtendimento { get; set; }
    public AtendimentoModel Atendimento { get; set; }

    public DateTime Criacao { get; set; }
    
    public DateTime Alteracao { get; set; }

    public bool IsCliente { get; set; }

    [MaxLength(2000)]
    public string Mensagem { get; set; }

    public long IdEmResposta { get; set; }
}