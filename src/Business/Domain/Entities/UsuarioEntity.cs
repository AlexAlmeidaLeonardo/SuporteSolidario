using SuporteSolidarioBusiness.Domain.Enums;

namespace SuporteSolidarioBusiness.Domain.Entities;

public class UsuarioEntity: BaseEntity
{
    public TipoUsuario TipoDeUsuario { get; set; }
    
    public string Login { get; set; }

    public string? Email { get; set; }

    public string? Celular { get; set; }

    public string Password1 { get; set; }

    public string? Password2 { get; set; }

    public bool Ativo { get; set; }


    public DateTime? Criacao { get; set; }

    public DateTime? Alteracao { get; set; }
}