using System.ComponentModel.DataAnnotations.Schema;
using SuporteSolidarioBusiness.Infrastructure.MySQL;

public class AtendimentoModel: BaseModel
{
    [Column(Order = 1)]
    [ForeignKey("Solicitacao")]
    public long IdSolicitacao { get; set; }
    public SolicitacaoModel Solicitacao { get; set; }

    [Column(Order = 2)]
    [ForeignKey("Colaborador")]
    public long IdColaborador { get; set; }
    public ColaboradorModel Colaborador { get; set; }

    public DateTime AtendidoEm { get; set; } = DateTime.Now;

    public DateTime? ConfirmadoEm { get; set; }

    public DateTime? FinalizadoEm { get; set; }

    public int? Avaliacao { get; set; }
}