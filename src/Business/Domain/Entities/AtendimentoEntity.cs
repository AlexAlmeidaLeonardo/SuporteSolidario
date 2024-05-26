namespace SuporteSolidarioBusiness.Domain.Entities;

public class AtendimentoEntity: BaseEntity
{
    public long IdSolicitacao { get; set; }

    public long IdColaborador { get; set; }

    public DateTime AtendidoEm { get; set; } = DateTime.Now;

    public DateTime? RecusadoEm { get; set; }

    public DateTime? ConfirmadoEm { get; set; }

    public DateTime? FinalizadoEm { get; set; }

    public int? Avaliacao { get; set; }
}