using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SuporteSolidarioBusiness.Infrastructure.MySQL;

public class SolicitacaoModel: BaseModel
{
    [Column(Order = 1)]
    [ForeignKey("Cliente")]    
    public long IdCliente { get; set; }
    public ClienteModel Cliente { get; set; }

    [Column(Order = 2)]
    [ForeignKey("Servico")]
    public long IdServico { get; set; }
    public ServicoModel Servico { get; set; }    

    public DateTime Data { get; set; }

    [MaxLength(4000)]
    public string Detalhes { get; set; }

    public DateTime DataServico { get; set; }

    [DefaultValue(true)]
    public bool EmAberto { get; set; }

    // Endereço
    [MaxLength(8)]
    public string Cep { get; set; }

    [MaxLength(20)]
    public string Logradouro { get; set; }

    [MaxLength(100)]
    public string Endereco { get; set; }
    public int Numero { get; set; }
    
    [MaxLength(100)]
    public string? Bairro { get; set; }

    [MaxLength(100)]
    public string Cidade { get; set; }

    [MaxLength(2)]
    public string Estado { get; set; }

    public double Latitude { get; set; }
    
    public double Longitude { get; set; }

}