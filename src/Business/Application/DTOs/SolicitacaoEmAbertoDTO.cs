namespace SuporteSolidarioBusiness.Application.DTOs;

public class SolicitacaoEmAbertoDTO
{
    public long Id { get; set; }

    public long IdCliente { get; set; }

    public long IdServico { get; set; }

    public string DescricaoCategoria { get; set; }

    public string DescricaoServico { get; set; }

    public DateTime Data { get; set; }

    public DateTime DataServico { get; set; }

    public string Detalhes { get; set; }
}