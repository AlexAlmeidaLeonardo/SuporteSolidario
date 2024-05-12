namespace SuporteSolidarioBusiness.Infrastructure.MySQL;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class BaseModel
{
    [Key]
    [Column(Order = 0)]
    public long Id { get; set; }
}