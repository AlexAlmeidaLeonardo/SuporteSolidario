namespace SuporteSolidarioBusiness.Infrastructure.MySQL;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ServicoModel: BaseModel
{
    [Column(Order = 1)]
    [ForeignKey("Categoria")]
    public long IdCategoria { get; set; }    
    public CategoriaModel Categoria { get; set; }

    [MaxLength(60)]
    public string Descricao { get; set; }
}
