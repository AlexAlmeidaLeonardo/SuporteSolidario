namespace SuporteSolidarioBusiness.Infrastructure.MySQL;

using System.ComponentModel.DataAnnotations;

public class CategoriaModel: BaseModel
{
    [MaxLength(60)]
    public string Descricao { get; set; }
}
