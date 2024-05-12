using System.ComponentModel.DataAnnotations;

namespace SuporteSolidario.ViewModel;

public class LoginViewModel
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Insira o seu usuário")]
    [Display(Name = "Usuário")]
    public string Login { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Senha")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Insira uma senha válida")]
    public string Password { get; set; }

    [Display(Name = "Lembrar-me")]
    public bool RememberMe { get; set; }
}