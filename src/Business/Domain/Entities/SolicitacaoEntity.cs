namespace SuporteSolidarioBusiness.Domain.Entities;

public class SolicitacaoEntity: BaseEntity
{
    public long IdCliente { get; set; }

    public long IdServico { get; set; }

    public DateTime Data { get; set; }

    public string Detalhes { get; set; }

    public DateTime DataServico { get; set; }

    public bool EmAberto { get; set; }


    // Endereço
    public string? Cep { get; set; }
    public string? Logradouro { get; set; }
    public string? Endereco { get; set; }
    public int? Numero { get; set; }
    public string? Bairro { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    
}