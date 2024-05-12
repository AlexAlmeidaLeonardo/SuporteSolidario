using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SuporteSolidarioBusiness.Domain.Enums;
using SuporteSolidarioBusiness.Infrastructure.MySQL;

public class UsuarioModel: BaseModel
{
    [Required]
    public TipoUsuario TipoDeUsuario { get; set; }

    [MaxLength(255)]
    public string? Login { get; set; }

    [MaxLength(254)]
    public string? Email { get; set; }

    [MaxLength(11)]
    public string? Celular { get; set; }

    [MaxLength(255)]
    public string? Hash { get; set; }

    [MaxLength(255)]
    public string? Salt { get; set; }

    [DefaultValue(true)]
    public bool Ativo { get; set; } = true;

    public DateTime Criacao { get; set; } = DateTime.Now;

    public DateTime? Alteracao { get; set; }
}