namespace SuporteSolidarioBusiness.Infrastructure.MySQL;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PessoaModel: BaseModel
{
    

    [MaxLength(50)]
    public string Nome { get; set; }
    
    [MaxLength(150)]
    public string Sobrenome { get; set; }
    
    [MaxLength(11)]
    public string Cpf { get; set; }

    // Endereço
    [MaxLength(8)]
    public string Cep { get; set; }

    [MaxLength(20)]
    public string Logradouro { get; set; }

    [MaxLength(100)]
    public string Endereco { get; set; }
    public int Numero { get; set; }
    
    [MaxLength(100)]
    public string Bairro { get; set; }

    [MaxLength(100)]
    public string Cidade { get; set; }

    [MaxLength(2)]
    public string Estado { get; set; }

    public double Latitude { get; set; }
    
    public double Longitude { get; set; }

    // Dados para contato
    [MaxLength(2)]
    public string DddTelefone { get; set; }
    
    [MaxLength(9)]
    public string Telefone { get; set; }

    [MaxLength(2)]
    public string DddCelular { get; set; }

    [MaxLength(9)]
    public string Celular { get; set; }

    [MaxLength(255)]
    public string Email { get; set; }

    // Dados de controle
    [DefaultValue(true)]
    public bool Ativo { get; set; }

    [DefaultValue(false)]
    public bool PossuiCoordenadasGps { get; set; }
    
    public DateTime Criacao { get; set; }
    
    public DateTime Alteracao { get; set; }

    [DefaultValue(null)]
    public DateTime? Inativacao { get; set; }

    [Column(Order = 1)]
    [ForeignKey("Usuario")]
    public long IdUsuario { get; set; }
    public UsuarioModel Usuario { get; set; }
}