using System.ComponentModel.DataAnnotations.Schema;
using SuporteSolidarioBusiness.Infrastructure.MySQL;

public class ColaboradorServicoModel: BaseModel
{
    [Column(Order = 1)]
    [ForeignKey("Colaborador")]    
    public long IdColaborador { get; set; }
    public ColaboradorModel Colaborador { get; set; }

    [Column(Order = 2)]
    [ForeignKey("Servico")]    
    public long IdServico { get; set; }
    public ServicoModel Servico { get; set; }

    
}