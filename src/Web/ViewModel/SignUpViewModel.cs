namespace SuporteSolidario.ViewModel;

using System.ComponentModel.DataAnnotations;
using SuporteSolidarioBusiness.Domain.Enums;

public class SignUpViewModel: BaseViewModel
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Insira o seu usuário")]
    [Display(Name = "Usuário")]
    public string Login { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Insira o seu e-mail")]
    [Display(Name = "E-mail")]
    public string Email { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Insira o seu celular")]
    [Display(Name = "Celular")]
    public string Celular { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Senha")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Insira uma senha válida")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Repita a Senha")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Repita sua senha")]
    public string Password2 { get; set; }

    [Display(Name = "Tipo de Usuário")]
    public TipoUsuario TipoDeUsuario { get; set; }
}