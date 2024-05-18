using System.ComponentModel.DataAnnotations;

namespace SuporteSolidario.ViewModel;

public class PessoaViewModel: BaseViewModel
{
    
    public long Id { get; set; }
    public long IdUsuario { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Insira o seu nome")]
    [Display(Name = "Nome")]
    public string Nome { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Insira o seu sobrenome")]
    [Display(Name = "Sobrenome")]
    public string Sobrenome { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Insira o seu CPF")]
    [Display(Name = "CPF")]
    public string Cpf { get; set; }

    // Endereço
    [Required(AllowEmptyStrings = false, ErrorMessage = "Insira o seu Cep")]
    [Display(Name = "Cep")]
    public string Cep { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Insira o seu Logradouro")]
    [Display(Name = "Logradouro")]
    public string Logradouro { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Insira o seu Endereco")]
    [Display(Name = "Endereco")]
    public string Endereco { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Insira o seu Numero")]
    [Display(Name = "Numero")]
    public int Numero { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Insira o seu Bairro")]
    [Display(Name = "Bairro")]
    public string Bairro { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Insira o seu Cidade")]
    [Display(Name = "Cidade")]
    public string Cidade { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Insira o seu Estado")]
    [Display(Name = "Estado")]
    public string Estado { get; set; }
    
    // Dados para contato
    [Required(AllowEmptyStrings = false, ErrorMessage = "Insira o seu Ddd do telefone")]
    [Display(Name = "DddTelefone")]
    public string DddTelefone { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Insira o seu Telefone")]
    [Display(Name = "Telefone")]
    public string Telefone { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Insira o seu Ddd do celular")]
    [Display(Name = "DddCelular")]
    public string DddCelular { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Insira o seu Celular")]
    [Display(Name = "Celular")]
    public string Celular { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Insira o seu Email")]
    [Display(Name = "Email")]
    public string Email { get; set; }

    public string FORM_ACTION { get; set; }
    
}